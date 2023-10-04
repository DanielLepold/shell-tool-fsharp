namespace ShellToolFsharp

open System
open ShellToolFsharp.Commands

module Program =
  // Naming the shell
  let prompt = "shell-prompt>"

  while true do
      Console.Write(prompt)
      let userInput = Console.ReadLine()

      // Trim whitespace and newline characters
      let trimmedInput = userInput.Trim()

      match trimmedInput.ToLower() with
      | "exit" -> Environment.Exit(0)
      | cmd when cmd.StartsWith("cd ") -> adjustNewDirectory cmd
      | "cd .." -> changeDirectoryToParent
      | _ -> processGeneralCommand trimmedInput

