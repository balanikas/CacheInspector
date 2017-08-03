using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace CacheInspector
{
    class CommandParser
    {
        public static Command ParseCommand(string command)
        {
            var parts = FormatCommand(command);
           
            var showDetails = parts.Contains("details", StringComparer.InvariantCultureIgnoreCase);
            var cmd = parts[0];
            
            switch (cmd)
            {
                case "help":
                {
                    return new Command {Cmd = CommandType.Help};
                }
                case "list":
                {
                    if (parts.Length == 1)
                    {
                        return new Command {Cmd = CommandType.List, ShowDetails = showDetails};
                    }
                    if (parts.Length >= 2)
                    {
                        return new Command { Cmd = CommandType.List, Key = parts[1] , ShowDetails = showDetails };
                    }
                    return new Command {Cmd = CommandType.Invalid};
                    
                }
                case "list-is":
                {
                    return new Command { Cmd = CommandType.ListInventorySystem, Key = "ep:ecf:is", ShowDetails = showDetails};
                }
                case "list-mo":
                {
                    return new Command { Cmd = CommandType.List, Key = "ep:ecf:mo", ShowDetails = showDetails};
                }
                case "list-pricing":
                {
                    return new Command { Cmd = CommandType.List, Key = "ep:catalogkeyprices", ShowDetails = showDetails};
                }
                case "list-warehouses":
                {
                    return new Command { Cmd = CommandType.List, Key = "ep:commerce:wi", ShowDetails = showDetails };
                }
                case "list-dto":
                {
                    return new Command { Cmd = CommandType.List, Key = "ecf-", ShowDetails = showDetails };
                }
                case "list-pages":
                {
                    return new Command { Cmd = CommandType.List, Key = "eppagedata:", ShowDetails = showDetails};
                }
                case "clear":
                {
                    if (parts.Length == 1)
                    {
                        return new Command { Cmd = CommandType.Clear };
                    }
                    if (parts.Length >= 2)
                    {
                        return new Command { Cmd = CommandType.Clear, Key = parts[1] };
                    }
                    return new Command { Cmd = CommandType.Invalid };
                }
                case "clear-is":
                {
                    return new Command { Cmd = CommandType.Clear, Key = "ep:ecf:is" };
                }
                case "clear-mo":
                {
                    return new Command { Cmd = CommandType.Clear, Key = "ep:ecf:mo" };
                }
                case "clear-pricing":
                {
                    return new Command { Cmd = CommandType.Clear, Key = "ep:catalogkeyprices" };
                }
                case "clear-warehouses":
                {
                    return new Command { Cmd = CommandType.Clear, Key = "ep:commerce:wi" };
                }
                case "clear-dto":
                {
                    return new Command { Cmd = CommandType.Clear, Key = "ecf-" };
                }
                case "clear-pages":
                {
                    return new Command { Cmd = CommandType.Clear, Key = "eppagedata:" };
                }
                default:
                {
                    return new Command { Cmd = CommandType.Invalid };
                }
            }
        }

        private static string[] FormatCommand(string command)
        {
            var regex = new Regex("[ ]{2,}", RegexOptions.None);
            command = regex.Replace(command, " ");
            return command.Trim().ToLower().Split(' ');
        }
    }
}
