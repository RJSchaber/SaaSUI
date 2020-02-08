using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Diagnostics;
using System.Threading;
using System.ServiceProcess;
using System.Management;
using System.Management.Instrumentation;
using System.Security.Principal;
using Cassia;
using System.Runtime.InteropServices;
using System.IO;


namespace SaaSUI
{
	public class ServerCommands : ServerMaintenance
	{	//Declare our global variables that we want to pass to every method here
		public string middletier {get; set; }
		public string serviceName {get; set; }
		public string selectedClient {get; set; }
		public string selectedCommand {get; set; }
		public DataTable mtTable {get; set; }
		public BackgroundWorker worker {get; set; }
		bool isHost;
		string client;
		string environment;
		ServiceController ssService;
		string commandResult;
		public DoWorkEventArgs workerArgs { get; set; }

		public void StartService()
		{
			try
			{
				ssService = new ServiceController(serviceName, middletier);
				if (middletier == "All")
				{
					bool result = CautionMessage();
					if (result)  //Dialog result is yes, proceed
					{
						foreach(DataRow mt in mtTable.Rows)
						{
							ExtractFromTable(mt);

							bool servicesExist = DoesServiceExist(middletier, serviceName);  //Check to see if the service exists. 
							if(servicesExist)
							{
								if(ssService.Status.Equals(ServiceControllerStatus.Running))  //If the service is already running, let the user know.
								{
									worker.ReportProgress(0, commandResult = serviceName + " is: " + ssService.Status.ToString() + " on " + 
										middletier + Environment.NewLine);  //To allow the application to update the status as well as run these methods simultaneously, we had to apply multi-threaded workers to carry these methods out. 
								}						   // Using the report progress method to update the form as the worker processes the desired method.  
								else if(isHost && serviceName == "ShadowServiceHost") //If its a host machine and the serviceName is our host service, start the host
								{
									ssService.Start();
									ssService.WaitForStatus(ServiceControllerStatus.Running);
									worker.ReportProgress(0, commandResult = serviceName + " is: " + ssService.Status.ToString() + " on "+ middletier + Environment.NewLine);
								}
								else if (serviceName != "ShadowServiceHost")   //if its not a host and not running, start it.  (The point here is that the host service exists on all machines but we dont want to start it on any that arent designated hosts.
								{
									ssService.Start();
									ssService.WaitForStatus(ServiceControllerStatus.Running);  //Wait for the service to start
									worker.ReportProgress(0, commandResult = serviceName + " is: " + ssService.Status.ToString() + " on "+ middletier + Environment.NewLine);
								}
								else
								{
									worker.ReportProgress(0, commandResult = middletier + " Is not a host machine.  Host will not be started. " + Environment.NewLine);
									SetServiceDisabled(middletier);
								}
							}
							else
							{
								worker.ReportProgress(0, commandResult = serviceName + " does not exist on " + middletier + Environment.NewLine);
							}
						}
					}
				}
				else if(DoesServiceExist(middletier, serviceName))  //if the middletier isnt set to all, start the service for the selected MT
				{
					bool result = CautionMessage();
					if (result)  //Dialog result is yes, proceed
					{
						if(ssService.Status.Equals(ServiceControllerStatus.Running))
						{
							worker.ReportProgress(0, commandResult = serviceName + " is: " + ssService.Status.ToString() + " on "+ middletier + Environment.NewLine);
						}
						else
						{
							ssService.Start();
							ssService.WaitForStatus(ServiceControllerStatus.Running);
							worker.ReportProgress(0, commandResult = serviceName + " is: " + ssService.Status.ToString() + " on "+ middletier + Environment.NewLine);
						}
					}
				}
				else
				{
					worker.ReportProgress(0, commandResult = serviceName + " does not exist on "+ serviceName + Environment.NewLine);
				}
			}
			catch(Exception ex)
			{
				throw new ApplicationException("Application encountered an error while attempting to start services: ", ex);
			}
		}
		public string StopService()  //Stop service is very similar to start service except the command is obviously stop instead of start
		{
			try
			{
				ssService = new ServiceController(serviceName, middletier);

				if (middletier == "All")
				{
					bool result = CautionMessage();
					if (result)
					{
						foreach(DataRow mt in mtTable.Rows)
						{
							ExtractFromTable(mt);
							bool serviceExists = DoesServiceExist(middletier, serviceName);
							if(serviceExists)
							{
								if(ssService.Status.Equals(ServiceControllerStatus.Stopped) || ssService.Status.Equals(ServiceControllerStatus.StopPending))
								{
									worker.ReportProgress(0, commandResult = serviceName + " is: " + ssService.Status.ToString() + " on "+ middletier + Environment.NewLine);
								}
								else
								{
									ssService.Stop();
									ssService.WaitForStatus(ServiceControllerStatus.Stopped);
									worker.ReportProgress(0, commandResult = serviceName + " is: " + ssService.Status.ToString() + " on "+ middletier + Environment.NewLine);
								}
							}
							else
							{
								worker.ReportProgress(0, commandResult = serviceName + " does not exist on "+ middletier + Environment.NewLine);
							}
						}
						return commandResult;
					}
					else return null;
				}
				else if(DoesServiceExist(middletier, serviceName))
				{
					bool result = CautionMessage();
					if (result)  //Dialog result is yes, proceed
					{
						if(ssService.Status.Equals(ServiceControllerStatus.Stopped) || ssService.Status.Equals(ServiceControllerStatus.StopPending))
						{
							worker.ReportProgress(0, commandResult = serviceName + " is: " + ssService.Status.ToString() + " on "+ middletier + Environment.NewLine);
							return commandResult;
						}
						else
						{
							ssService.Stop();
							ssService.WaitForStatus(ServiceControllerStatus.Stopped);
							worker.ReportProgress(0, commandResult = serviceName + " is: " + ssService.Status.ToString() + " on "+ middletier + Environment.NewLine);
							return commandResult;
						}
					}
					else return null;
					
				}
				else
				{
					worker.ReportProgress(0, commandResult = serviceName + " does not exist on "+ serviceName + Environment.NewLine);
					return commandResult;
				}
			}
			catch(Exception ex)
			{
				throw new ApplicationException("Application encountered an error while attempting to stop services: ", ex);
			}
		}
		public string KillService()
		{
			try
			{
				if (middletier == "All")
				{
					bool result = CautionMessage();
					if (result)
					{
						foreach(DataRow mt in mtTable.Rows)
						{
							ExtractFromTable(mt);
							if(ssService.Status.Equals(ServiceControllerStatus.Running))  //If the service exists, look for it's process thats running and terminate it
							{
								worker.ReportProgress(0, commandResult = serviceName + " status is: " + ssService.Status.ToString() + " on " + middletier + "... " + Environment.NewLine);
								ManagementScope scope = new ManagementScope("\\\\"+ middletier + "\\root\\cimv2");  //directory for processes
								scope.Connect(); 
								ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_Process WHERE Name='" + serviceName + ".exe'");  //select everything from the running processes that matches our service
								ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);  
								ManagementObjectCollection objectCollection = searcher.Get();  
								foreach(ManagementObject managementObject in objectCollection) 
								{
									managementObject.InvokeMethod("Terminate", null);
								}
								Thread.Sleep(500);
								worker.ReportProgress(0, commandResult = serviceName + " status is: " + ssService.Status.ToString() + " on " + middletier + "... " + Environment.NewLine);
							}
							else
							{
								commandResult+= serviceName + " is: " + ssService.Status.ToString() + " on "+ middletier + Environment.NewLine;
							}
						}
						return commandResult;
					}
					else return null;
				}
				else //If middletier selection isnt all, kill the process for the selected middletier.
				{
					bool result = CautionMessage();
					if (result)  //Dialog result is yes, proceed
					{
						ssService = new ServiceController(serviceName, middletier);
						if(ssService.Status.Equals(ServiceControllerStatus.Running))
						{
							worker.ReportProgress(0, commandResult = serviceName + " is: " + ssService.Status.ToString() + " on "+ middletier + Environment.NewLine);
							ManagementScope scope = new ManagementScope("\\\\" + middletier + "\\root\\cimv2");
							scope.Connect();
							ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_Process WHERE Name='" + serviceName + ".exe'");
							ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
							ManagementObjectCollection objectCollection = searcher.Get();

							foreach(ManagementObject managementObject in objectCollection)
							{
								managementObject.InvokeMethod("Terminate", null);
							}
							Thread.Sleep(500);
							worker.ReportProgress(0, commandResult = serviceName + " is: " + ssService.Status.ToString() + " on "+ middletier + Environment.NewLine);
							return commandResult;
						}
						else
						worker.ReportProgress(0, commandResult = serviceName + " is: " + ssService.Status.ToString() + " on "+ middletier + Environment.NewLine);
						return commandResult;
					}
					else return null;
				}
			}
			catch(Exception ex)
			{
				throw new ApplicationException("Application encountered an error while attempting to kill services: ", ex);
			}
		}

		public string ServiceStatus()
		{
			try
			{
				if (middletier == "All")
				{
					bool result = CautionMessage();
					if (result)
					{
						foreach(DataRow mt in mtTable.Rows)
						{
							ExtractFromTable(mt);
							bool serviceExists = DoesServiceExist(middletier, serviceName);
							if(serviceExists)
							{
								worker.ReportProgress(0, commandResult = serviceName + " is: " + ssService.Status.ToString() + " on "+ middletier + Environment.NewLine);
							}
							else
							{
								worker.ReportProgress(0, commandResult = serviceName + " does not exist on "+ middletier + Environment.NewLine);
							}
						}
					}
					else
						return null;
				}
				else
				{
					bool serviceExists = DoesServiceExist(middletier, serviceName);
					if(serviceExists)
					{
						bool result = CautionMessage();
						if (result)  //Dialog result is yes, proceed
						{
							ssService = new ServiceController(serviceName, middletier);
							worker.ReportProgress(0,commandResult = serviceName + " is: " + ssService.Status.ToString() + " on "+ middletier + Environment.NewLine);
						}
						else return null;
					}
					else
					{
						worker.ReportProgress(0, commandResult = serviceName + " does not exist on " + serviceName + Environment.NewLine);
					}
				}

				return commandResult;
			}
			catch(Exception ex)
			{
				throw new ApplicationException("Application encountered an error while attempting to check service status: ", ex);
			}
		}

		public string LoggedUsers()
		{
			try
			{
				ITerminalServicesManager manager = new TerminalServicesManager();
				if (middletier == "All")
				{
					bool result = CautionMessage();
					if (result)
					{
						foreach(DataRow mt in mtTable.Rows)
						{
							ExtractFromTable(mt);
							using (ITerminalServer server = manager.GetRemoteServer(middletier)) //Connects to the remote terminal server requested
							{
								server.Open();
								foreach (ITerminalServicesSession session in server.GetSessions()) //Pulls all of the logged in user sessions
								{
									NTAccount account = session.UserAccount;
									if (account != null)  
									{
										worker.ReportProgress(0, commandResult = account + " is active on " + middletier + Environment.NewLine);
									}
								}
							}
						}
						return commandResult;
					}
					else 
					return commandResult;
				}
				else
				{
					bool result = CautionMessage();
					if (result)  //Dialog result is yes, proceed
					{
						using (ITerminalServer server = manager.GetRemoteServer(middletier))
						{
							server.Open();
							foreach (ITerminalServicesSession session in server.GetSessions())
							{
								NTAccount account = session.UserAccount;
								if (account != null)
								{
									worker.ReportProgress(0, commandResult = account + " is active on " + middletier + Environment.NewLine);
								}
							}
						}
						return commandResult;
					}
					else
					return commandResult;
				}
			}
			catch(Exception ex)
			{
				throw new ApplicationException("Application encountered an error while attempting to check logged in users: ", ex);
			}
		}

		public string LogOffUsers()  //Almost exactly the same as the query for logged in users except we log off each session
 		{
			try
			{
				ITerminalServicesManager manager = new TerminalServicesManager();
				if (middletier == "All")
				{
					bool result = CautionMessage();
					if (result)
					{
						foreach(DataRow mt in mtTable.Rows)
						{
							ExtractFromTable(mt);
							using (ITerminalServer server = manager.GetRemoteServer(middletier))
							{
								server.Open();
								foreach (ITerminalServicesSession session in server.GetSessions())
								{
									NTAccount account = session.UserAccount;
									if (account != null)
									{
										session.Logoff();
										worker.ReportProgress(0, commandResult = account + " is being logged off of " + middletier + Environment.NewLine);
									}
								}
							}
						}
						return commandResult;
					}
					else
						return commandResult;
				}
				else
				{
					bool result = CautionMessage();
					if (result)  //Dialog result is yes, proceed
					{
						using (ITerminalServer server = manager.GetRemoteServer(middletier))
						{
							server.Open();
							foreach (ITerminalServicesSession session in server.GetSessions())
							{
								NTAccount account = session.UserAccount;
								if (account != null)
								{
									session.Logoff();
									worker.ReportProgress(0, commandResult = account + " is being logged off of " + middletier + Environment.NewLine);
								}
							}
						}
						return commandResult;
					}
					else
						return commandResult;
				}
			}
			catch(Exception ex)
			{
				throw new ApplicationException("Application encountered an error while attempting to log users off of middletiers: ", ex);
			}
		}

		public string RestartServer()
		{
			try
			{
				if (middletier == "All")
				{
					bool result = CautionMessage();
					if (result)
					{
						foreach(DataRow mt in mtTable.Rows)
						{
							ExtractFromTable(mt);
							string strShutdown = @"/c shutdown -r -t 1 -f -m \\" + middletier;  // -r = restart, 
							ProcessStartInfo servShutdown = new ProcessStartInfo("cmd.exe",strShutdown);  //start a command prompt and put in the provided string
							servShutdown.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.System);  //working directory for the command prompt  
							servShutdown.WindowStyle = ProcessWindowStyle.Hidden;
							servShutdown.RedirectStandardOutput = true;
							servShutdown.UseShellExecute = false;
							servShutdown.CreateNoWindow = true;
							Process procCommand = Process.Start(servShutdown);
							procCommand.WaitForExit();
							worker.ReportProgress(0, commandResult = middletier + " is restarting" + Environment.NewLine);
						}
						return commandResult;
					}
					else
						return commandResult;
				}
				else
				{
					bool result = CautionMessage();
					if (result)  //Dialog result is yes, proceed
					{
						string strShutdown = @"/c shutdown -r -t 1 -f -m \\" + middletier;
						ProcessStartInfo servShutdown = new ProcessStartInfo("cmd.exe",strShutdown);
						servShutdown.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.System);
						servShutdown.WindowStyle = ProcessWindowStyle.Hidden;
						servShutdown.RedirectStandardOutput = true;
						servShutdown.UseShellExecute = false;
						servShutdown.CreateNoWindow = true;
						Process procCommand = Process.Start(servShutdown);
						procCommand.WaitForExit();
						worker.ReportProgress(0, commandResult = middletier + " is restarting" + Environment.NewLine);
						return commandResult;
					}
					else
						return commandResult;
				}
			}
			catch(Exception ex)
			{
				throw new ApplicationException("Application encountered an error while attempting to restart servers: ", ex);
			}
		}

		public string ReplaceDLLs()
		{
			try
			{
				if (middletier == "All")
				{
					bool result = CautionMessage();
					if (result)
					{
						foreach(DataRow mt in mtTable.Rows)
						{
							ExtractFromTable(mt);

							String mask = "*.dll";
							String source = @"C:\Users\rschaber\Desktop\SaaSUI\DLLs\" + client + @"\" + environment;
							String destination = @"\\" + middletier + @"\c$\Users\rschaber\";
							string tempFileName = "";

							String[] files = Directory.GetFiles(source, mask, SearchOption.AllDirectories);

							int collectedFiles = files.Count();

							foreach (String file in files)
							{	
								string fileNameOnly = Path.GetFileNameWithoutExtension(file);
								string extension = Path.GetExtension(file);
								string path = Path.GetDirectoryName(file);
								string newpath = destination + fileNameOnly + extension;
								int count = 1;

								if (File.Exists(newpath))
								{
									while(File.Exists(newpath))
									{
										tempFileName = string.Format("{0}({1})", fileNameOnly, count++);
										newpath = destination + tempFileName + ".old";
									}
									File.Move(destination + fileNameOnly + extension, newpath);
								}
								File.Copy(file, destination + new FileInfo(file).Name);
								worker.ReportProgress(0, commandResult = Environment.NewLine);
								worker.ReportProgress(0, commandResult = "Copied " + fileNameOnly + " to " + destination + Environment.NewLine);
							}
						}
						return commandResult;
					}
					else
						return commandResult;
				}
				else
				{
					string client = (from DataRow dr in mtTable.Rows where (string)dr["ServerName"] == middletier select (string)dr["Client"]).FirstOrDefault();  //Navigate mtTable to pull the client name so that we can navigate to their personal dll folder 
					string environment = (from DataRow dr in mtTable.Rows where (string)dr["ServerName"] == middletier select (string)dr["EnvironmentType"]).FirstOrDefault();
					String mask = "*.dll";
					String source = @"C:\Users\rschaber\Desktop\SaaSUI\DLLs\" + client + @"\" + environment;
					String destination = @"\\"+middletier+ @"\c$\Users\rschaber\";
					string tempFileName = "";

					String[] files = Directory.GetFiles(source, mask, SearchOption.AllDirectories);
					int collectedFiles = files.Count();

					DialogResult result = MessageBox.Show("You have " + collectedFiles + " files in the " + source + " folder.  You are moving them to " 
						+ middletier + " in " + client + " " + environment+ " Would you like to continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
					if (result == DialogResult.Yes)
					{
						foreach (String file in files)
						{	
							string fileNameOnly = Path.GetFileNameWithoutExtension(file);
							string extension = Path.GetExtension(file);
							string path = Path.GetDirectoryName(file);
							string newpath = destination + fileNameOnly + extension;
							int count = 1;

							if (File.Exists(newpath))
							{
								while(File.Exists(newpath))
								{
									tempFileName = string.Format("{0}({1})", fileNameOnly, count++);
									newpath = destination + tempFileName + ".old";
								}
								File.Move(destination + fileNameOnly + extension, newpath);
							}
							File.Copy(file, destination + new FileInfo(file).Name);
							worker.ReportProgress(0, commandResult = Environment.NewLine);
							worker.ReportProgress(0, commandResult = "Copied " + fileNameOnly + " to " + destination + Environment.NewLine);
						}
						return commandResult;
					}
					else
					{
						return commandResult;
					}
				}
			}
			catch(Exception ex)
			{
				throw new ApplicationException("Application encountered an error while attempting to replace DLLs: ", ex);
			}
		}

		bool DoesServiceExist(string machineName, string serviceName)
		{
		   return ServiceController.GetServices(machineName).Any(serviceController => serviceController.ServiceName.Equals(serviceName));
		}

		public void QueryHighCPU(object sender, DoWorkEventArgs e)
		{
			PerformanceCounter processCPUCounter;
			Process[] ssDispatchers = Process.GetProcessesByName("ShadowJobDispatcher", middletier);

			foreach (var process in ssDispatchers)
			{
				//totalCPUCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
				//processCPUCounter = new PerformanceCounter("Process", "% Processor Time", "firefox");
				//PerformanceCounter ramCounter = new PerformanceCounter("Memory", "Available MBytes", String.Empty, middleTier);
				//PerformanceCounter pageCounter = new PerformanceCounter("Paging File", "% Usage", "_Total", middleTier);

				//totalCPUCounter.NextValue();
				//processCPUCounter.NextValue();
				//ramCounter.NextValue();
				//pageCounter.NextValue();

				//string procName = Convert.ToString(process.ProcessName);
				if (worker.CancellationPending == true)
				{
					//worker.ReportProgress(0, "Task Cancelled. " + Environment.NewLine);
					e.Cancel = true;
					break;
				}
				else
				{
				int processID = process.Id;

				string perfInstance = GetProcessInstanceName(processID, middletier);

					processCPUCounter = new PerformanceCounter("Process", "% Processor Time", perfInstance, middletier);

					processCPUCounter.NextValue();

					Thread.Sleep(500);

					//totalCPUCounter.NextValue();
					//processCPUCounter.NextValue();
					//
					//Thread.Sleep(1000);
					//
					//int totalUsage = Convert.ToInt32(totalCPUCounter.NextValue());
					int processUsage = Convert.ToInt32(processCPUCounter.NextValue()/Environment.ProcessorCount);
					////int percentUsed = processUsage/totalUsage;
					//
					////var cpuUsage = String.Format("{0:##0} %", theCPUCounter.NextValue()/1024);
					////var ramUsage = String.Format("{0} MB", theCPUCounter.NextValue()/1024);
					////var pageFileUsage = String.Format("{0:##0} %", theCPUCounter.NextValue() / 1024);
					//
					worker.ReportProgress(0, middletier + Environment.NewLine + "Process Name: " + perfInstance + " -- PID: " + processID + " -- CPU Usage " + processUsage + "%" + Environment.NewLine);
					//commandresult += "Name: " + procName + "  PID: " + processID + "  Memory Usage: " + ramUsage + " CPU Usage: " + cpuUsage + " Page File Size: " + pageFileUsage + Environment.NewLine;
				}	
			}
		}

		private static string GetProcessInstanceName(int pid, string middleTier)
		{
			PerformanceCounterCategory processes = new PerformanceCounterCategory("Process", middleTier);
			try
			{
				string[] instances = processes.GetInstanceNames();
				foreach (string instance in instances)
				{
					using (PerformanceCounter cnt = new PerformanceCounter("Process", "ID Process", instance, middleTier))
					{
					      int val = (int) cnt.RawValue;
					      if (val == pid)
					      {
					         return instance;
					      }
					}
				}
				throw new Exception("Could not find performance counter " + 
				    "instance name for current process. This is truly strange ...");
			}
			catch(Exception ex)
			{
				throw new ApplicationException("Application encountered an error while attempting to get process instance name. ", ex);
			}
		}

		public void SetServiceDisabled(string serverName) 
		{
			ManagementPath myPath = new ManagementPath();
			ManagementBaseObject outParams = null;
			myPath.Server = serverName;
			myPath.NamespacePath = "\\\\" + serverName + "\\root\\cimv2";
			myPath.RelativePath = "Win32_Service.Name='" + serviceName + "'";
			using (ManagementObject service = new ManagementObject(myPath)) 
			{
				ManagementBaseObject inputArgs = service.GetMethodParameters("ChangeStartMode");
				inputArgs["startmode"] = "Disabled";
				outParams = service.InvokeMethod("ChangeStartMode", inputArgs, null);
			}
		}

		private bool CautionMessage()
		{
			string dialogMessage = String.Format("You are about to {0} on {2} for {1}. Would you like to continue?", selectedCommand, selectedClient, middletier);

			DialogResult result = MessageBox.Show(dialogMessage, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);  //Warn the user of what they are about to do.
					if (result == DialogResult.Yes)  //Dialog result is yes, proceed
					{
						return true;
					}
					else return false;
		}

		public void ExtractFromTable(DataRow mt)
		{
			isHost = Convert.ToBoolean(mt["Host"]);  //Check to see if the server is a host
			middletier = Convert.ToString(mt["ServerName"]);  //Since we're running this command for all boxes we have to go through the datatable by row and pull each servername
			ssService = new ServiceController(serviceName, middletier);  //Since we're pulling a new sever name by row, we have to recall this controller to connect to the right server
			client = Convert.ToString(mt["Client"]);
			environment = Convert.ToString(mt["EnvironmentType"]);
		}

	}
}
