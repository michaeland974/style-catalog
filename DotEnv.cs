public static class DotEnv
{
  public static void Load(string filePath){
    if (!File.Exists(filePath)){
      return;
    }
    foreach (var line in File.ReadAllLines(filePath)){
      int index = line.IndexOf('=', 0, line.Length-1);
      string key = line.Substring(0, index);
      string value = line.Substring(index+1);

      Environment.SetEnvironmentVariable(key, value);
      }
  }
}

