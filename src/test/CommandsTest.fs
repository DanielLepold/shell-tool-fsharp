module Test.CommandsTest

open System
open System.IO
open NUnit.Framework
open ShellToolFsharp.Commands

[<TestFixture>]
type AdjustNewDirectoryTests () =
    let tempDir = Path.GetTempPath()

    [<Test>]
    member _.``Adjusting New Directory Should Succeed`` () =
        let originalDir = Directory.GetCurrentDirectory()
        // Preconditions
        let testDir = Path.Combine(tempDir, "TestDir")
        Directory.CreateDirectory(testDir) |> ignore
        let cmd = $"cd {testDir}"

        // Process
        adjustNewDirectory cmd

        // Get the actual current directory
        let currentDirectory = Directory.GetCurrentDirectory()
        // Assert
        Assert.IsTrue(currentDirectory.Contains testDir)

        // Readjusting original state
        Environment.CurrentDirectory <- originalDir
        Directory.Delete testDir

    [<Test>]
    member _.``Adjusting Nonexistent Directory Should Display Error`` () =
        // Preconditions
        let nonexistentDir = "NonexistentDir"
        let cmd = "cd " + nonexistentDir
        let expectedErrorMessage = $"Directory '{nonexistentDir}' does not exist."

        // Redirect standard output to capture console output
        let writer = new StringWriter()
        Console.SetOut(writer)

        // Process
        adjustNewDirectory cmd

        // Restore standard output
        Console.SetOut(Console.Out)

        // Get the actual current directory
        let currentDirectory = Directory.GetCurrentDirectory()
        // Assert
        Assert.IsFalse(currentDirectory.Contains nonexistentDir)
        // Get the output from console
        let output = writer.ToString()
        //Assert
        Assert.IsTrue(output.Contains expectedErrorMessage)

[<TestFixture>]
type ChangeDirectoryToParentTests () =
    let tempPath = Path.GetTempPath()

    // Test case for changing to the parent directory
    [<Test>]
    member _.``Change to Parent Directory Should Succeed`` () =
        let originalDir = Environment.CurrentDirectory
        // Preconditions
        let childDir = Path.Combine(tempPath, "ChildDir")
        Directory.CreateDirectory(childDir) |> ignore
        Environment.CurrentDirectory <- childDir

        // Process
        changeDirectoryToParent

        let currentDir = Directory.GetCurrentDirectory()
        // Assert
        Assert.IsTrue(currentDir.Contains (tempPath.Substring(0, tempPath.Length - 1)))

        // Cleanup dir
        Environment.CurrentDirectory <- originalDir
        Directory.Delete childDir

    // Test case for attempting to change to the parent directory from root
    [<Test>]
    member _.``Change to Parent Directory from Root Should Show Message`` () =
         // Preconditions
         Environment.CurrentDirectory <- "/" // Root directory

         // Redirect standard output to capture console output
         let writer = new StringWriter()
         Console.SetOut(writer)
         // Process
         changeDirectoryToParent

         // Restore standard output
         Console.SetOut(Console.Out)

         let output = writer.ToString()
         let expectedMessage = "Already at the root directory."
         Assert.IsTrue(output.Contains expectedMessage)

[<TestFixture>]
type ProcessGeneralCommandTests () =

    [<Test>]
    member _.``Executing pwd Command Should Return Directory Path`` () =
        // Preconditions
        let command = "pwd"
        // Redirect standard output to capture console output
        let writer = new StringWriter()
        Console.SetOut(writer)
        // Process
        processGeneralCommand command
        // Restore standard output
        Console.SetOut(Console.Out)

        let output = writer.ToString()
        let expectedOutput = Directory.GetCurrentDirectory()
        // Assert
        Assert.IsTrue(output.Contains expectedOutput)

    [<Test>]
    member _.``Executing echo Command Should Return Echoed String`` () =
        // Preconditions
        let command = "echo Hello, World!"

        // Redirect standard output to capture console output
        let writer = new StringWriter()
        Console.SetOut(writer)
        // Process
        processGeneralCommand command

        let output = writer.ToString()
        let expectedOutput = "Hello, World!"
        // Assert
        Assert.IsTrue(output.Contains expectedOutput)
