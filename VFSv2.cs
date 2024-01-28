using System;
using System.Collections.Generic;
using System.Linq;
//by ZinderXLive (github: Dafash Client)
namespace System.IO.VFSv2 {
    class VFSv2 {
        public static Dictionary<string, object> main_system;
        public static Dictionary<string, object> main_dirs;
        public static void Init() { 
            main_system = new Dictionary<string, object>();
            main_dirs = new Dictionary<string, object>();
            main_dirs.Add("Sys","folder");
            main_dirs.Add(@"Notepad", "folder");
            main_dirs.Add("User", "folder"); }
        public static string[] GetFiles(string path) {
            string[] values = GetFiles_OLD();
            string[] files = new string[values.Length];
            if (Instruments.normPath(path)) { int i = 0;
            foreach (string value in values) {
          if (value.Contains(Instruments.CorPath(path))){
           string norm_file = value.Replace(Instruments.CorPath(path), "");
                 files[i] = norm_file; i++; }}
          for (int j = 0; j < files.Length; j++) {
                if (string.IsNullOrEmpty(files[j])) {
                    files[j] = files[files.Length - 1];
                      files = files.Where(s => 
               !string.IsNullOrEmpty(s)).ToArray(); } }
           return files; }else{ return new string[1] { "No Files" }; } }
        public static string[] GetDirs() {
     string[] dirs = new string[main_dirs.Count]; int i = 0;
    foreach(string dir in main_dirs.Keys) { dirs[i] = dir; i++; }
            return dirs; }
        public static string[] GetFiles_OLD(){
            string[] files = new string[main_system.Count];
            int i = 0; foreach (var file in main_system.Keys)
            { if (i < main_system.Count) {
        files[i] = file; i++; } else { break; } } return files;}
    public static void DebugError(string error) { Console.WriteLine(error); } }
    class File {
        public static void Create(string path, string filename){
            if (Instruments.normPath(Instruments.CorPath(path))){
                string norm_name = Instruments.CorName(filename);
            VFSv2.main_system.Add(Instruments.CorPath(path)+norm_name,"");}}
        public static void WriteAllText(string path, string filename, string text) {
          if (Instruments.normPath(Instruments.CorPath(path))){
                string norm_name = Instruments.CorName(filename);
                if (Exists(Instruments.CorPath(path), norm_name)) {
            VFSv2.main_system[Instruments.CorPath(path)+norm_name] = text;}else{
          VFSv2.main_system.Add(Instruments.CorPath(path)+norm_name, text); }}}
        public static string ReadAllText(string path,string filename){
            if (Instruments.normPath(Instruments.CorPath(path))){
              string norm_name = Instruments.CorName(filename);
       return VFSv2.main_system[Instruments.CorPath(path)+norm_name].ToString();
            }else{ return "ERROR!"; }}
        public static bool Exists(string path,string filename) {
           if (Instruments.normPath(Instruments.CorPath(path))){
                string norm_name = Instruments.CorName(filename);
     return VFSv2.main_system.ContainsKey(Instruments.CorPath(path)+norm_name);
            } else { return false; } }
        public static void Delete(string path, string filename) {
            if (Instruments.normPath(Instruments.CorPath(path))) {
                string norm_name = Instruments.CorName(filename);
        VFSv2.main_system.Remove(Instruments.CorPath(path)+norm_name); } } }
    class Instruments {
        public static string CorPath(string path) {
  if (path[path.Length-1]!='\\') { path += '\\'; } return path; }
        public static string CorName(string filename) {
            string corred = filename.Replace("\\", "").Replace(":", "").
            Replace("(", "").Replace(")", "").Replace("[", "").Replace("]", "").
            Replace("{", "").Replace("}", "").Replace(",","").Replace("/","");
            return corred; }
        public static bool normPath(string path) {
            string[] values = path.Split('\\'); if(values.Length == 2) { 
                return VFSv2.main_dirs.ContainsKey(values[0]); }
            else { bool[] bools = new bool[values.Length-1];
                for(int i = 0; i < bools.Length - 1;) {
                    bools[i] = VFSv2.main_dirs.ContainsKey(values[i]);
               i++; } return bools.All(value => value); } } }
    class Directory {
       public static bool Exists(string name) {
           return VFSv2.main_dirs.ContainsKey(
         Instruments.CorName(name)); }
        public static void Create(string name) {
           if (Exists(name)) { VFSv2.DebugError("Error!"); }
             if (!Exists(name)) { VFSv2.main_dirs.Add(
           Instruments.CorName(name), "folder"); } }
        public static void Delete(string name) {
  if (Exists(Instruments.CorName(name))) { VFSv2.main_dirs.Remove(
     Instruments.CorName(name)); string[] files= 
           VFSv2.GetFiles(Instruments.CorPath(name));
       if(files.Length>0 && files[0]!="No Files") {
        string[] files_old = VFSv2.GetFiles_OLD();
              foreach(string file in files_old) {
        if (file.Contains(Instruments.CorPath(name))){
           VFSv2.main_system.Remove(file); }}}}}}}
