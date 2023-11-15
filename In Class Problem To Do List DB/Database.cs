using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace In_Class_Problem_To_Do_List_DB
{
    public class Database
    {
        public const string connFilename = "InvoiceDB.db3";

        public const SQLite.SQLiteOpenFlags Flags =
        // open the conn in read/write mode
                    SQLite.SQLiteOpenFlags.ReadWrite |
        // create the conn if it doesn't exist
                    SQLite.SQLiteOpenFlags.Create |
        // enable multi-threaded conn access
                    SQLite.SQLiteOpenFlags.SharedCache;

        public static SQLite.SQLiteAsyncConnection conn;


        public static async Task Init()
        {
            if (conn is not null)
                return;

            string connPath = Path.Combine(FileSystem.AppDataDirectory, connFilename);

            conn = new SQLiteAsyncConnection(connPath, Flags);

            _ = await conn.CreateTableAsync<ToDoItem>();
        }

        public static async Task<List<ToDoItem>> GetAllItemsAsync()
        {
            await Init();
            return await conn.Table<ToDoItem>().ToListAsync();
        }

        public static async Task<int> SaveItemAsync(ToDoItem item)
        {
            await Init();
            if (item.Id != 0)
                return await conn.UpdateAsync(item);
            else
                return await conn.InsertAsync(item);
        }
    }
}