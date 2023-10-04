namespace ShellToolFsharp

open System

module Commands =
  // Handle "cd newFolder" command
  let adjustNewDirectory (cmd : string) =
         let directoryName = cmd.Substring(3).Trim()
         let newDir = System.IO.Path.Combine(Environment.CurrentDirectory, directoryName)
         match System.IO.Directory.Exists(newDir) with
         | true ->
             Environment.CurrentDirectory <- newDir
         | false ->
             Console.WriteLine($"Directory '{directoryName}' does not exist.")
  // Move up one directory level for both shell and system process
  let changeDirectoryToParent=
    let parentDirectory = System.IO.Directory.GetParent(Environment.CurrentDirectory)
    match parentDirectory with
    | null -> Console.WriteLine("Already at the root directory.")
    | _ ->
        Environment.CurrentDirectory <- parentDirectory.FullName
  // Process general commands
  let processGeneralCommand trimmedInput =
    let cmd = "/bin/bash"
    let processInfo = new System.Diagnostics.ProcessStartInfo(cmd, "-c \"" + trimmedInput + "\"")
    processInfo.RedirectStandardOutput <- true
    processInfo.UseShellExecute <- false
    processInfo.CreateNoWindow <- true

    let cmdProcess = new System.Diagnostics.Process()
    cmdProcess.StartInfo <- processInfo
    cmdProcess.Start() |> ignore

    let output = cmdProcess.StandardOutput.ReadToEnd()
    Console.WriteLine(output)

    cmdProcess.WaitForExit()
