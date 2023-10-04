# shell-tool-fsharp

This shell tool was written in F#, and it allows you to execute various commands on the operating system, commands like "ls -la," "pwd," "cd ..," "echo," "date," "cat," "man," and more. You can even chain commands together using pipes.

If you provide incorrect input, the tool will display an error message on the screen to guide you. To exit the tool, simply enter the "exit" command.

```shell
shell-prompt> exit
```
Additionally, the solution includes unit tests to ensure its reliability and functionality.

Please check the following examples.
## Examples
### Echo
```shell
shell-prompt> echo Hello, world!
Hello, world!
```
### Working directory changes
```shell
shell-prompt> cd /Users/daniel
shell-prompt> pwd
/Users/daniel
shell-prompt> cd ..
shell-prompt> pwd
/Users
```

### Piping
```shell
shell-prompt> date | tee date.txt -a | cut -d "," -f 2
Wed Oct  4 17:08:23 CEST 2023
```
Check the created date.txt file.
```shell
shell-prompt> cat date.txt
Wed Oct  4 17:08:23 CEST 2023
```
### Unvalid command
```shell
shell-prompt> runMyCommand
/bin/bash: run: command not found
```





