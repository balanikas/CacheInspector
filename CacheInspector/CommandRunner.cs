using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheInspector
{
    class CommandRunner
    {
        public static string RunCommand(Command command)
        {
            switch (command.Cmd)
            {
                case CommandType.Help:
                    return GetHelpText();
                case CommandType.List:
                    return CacheHandler.GetCache(command);
                case CommandType.ListInventorySystem:
                    return CacheHandler.GetCache(command);
                case CommandType.Invalid:
                    return $"invalid command: {command}";
                case CommandType.Clear:
                    return CacheHandler.ClearCache(command);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        static string GetHelpText()
        {
            return
                $"{"help",-30}{"shows help",-10}\n" +
                $"{"[Enter]",-30}{"exits",-10}\n" +
                $"{"cls",-30}{"clears console",-10}\n" +
                
                $"{"list",-30}{"lists all",-10}\n" +
                $"{"list [key]",-30}{"lists all beginning with [key]",-10}\n" +
                $"{"list-ls",-30}{"lists inventory system",-10}\n" +
                $"{"list-pricing",-30}{"lists pricing",-10}\n" +
                $"{"list-warehouses",-30}{"lists warehouses",-10}\n" +
                $"{"list-dto",-30}{"lists cached dtos",-10}\n" +
                $"{"list-mo",-30}{"lists metaobjects",-10}\n" +
                $"{"list-pages",-30}{"lists pagedata",-10}\n" +


                $"{"clear",-30}{"clears all",-10}\n" +
                $"{"clear [key]",-30}{"clears all beginning with [key]",-10}\n" +
                $"{"clear-is",-30}{"clears inventory system",-10}\n" +
                $"{"clear-mo",-30}{"clears metaobjects",-10}\n" +
                $"{"clear-pricing",-30}{"clears pricing",-10}\n" +
                $"{"clear-warehouses",-30}{"clears warehouses",-10}\n" +
                $"{"clear-pages",-30}{"clears pagedata",-10}\n" +
                $"{"clear-dto",-30}{"clears the dto cache",-10}\n";


        }
    }
}
