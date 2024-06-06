using System.Diagnostics;
using System.Formats.Asn1;
using System.Reflection;
using System.Security.AccessControl;

// Following code is to clean up the database upon program startup
// Directory.CreateDirectory("Data");
// using (StreamWriter w = File.AppendText("Data/ScreeningDB.json")); //create file if it doesn't already exist
// using (StreamWriter w = File.AppendText("Data/MovieDB.json"));
// using (StreamWriter w = File.AppendText("Data/AuditoriumDDB.json"));
//  using (StreamWriter w = File.AppendText("Data/UserDB.csv"));


Helper.DataBaseCleanup();
// Start actual program
Interface.GeneralMenu();

