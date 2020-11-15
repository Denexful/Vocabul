using System;
using System.Collections.Generic;
using System.Data;
using SQLite;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Vocabul.Classes;
using Vocabul.Models;
using System.IO;
using System.Reflection;
using Xamarin.Forms;
using System.Threading;

namespace Vocabul.Service
{
    class DatabaseService
    {

        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            if (File.Exists(App.FilePath))
            {
                return new SQLiteAsyncConnection(App.FilePath, Constants.Flags, true);

            }
            else
            {
                return new SQLiteAsyncConnection(App.FilePath, Constants.Flags);
            }
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public DatabaseService()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Modul).Name) && !Database.TableMappings.Any(m => m.MappedType.Name == typeof(Vocabulary).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Modul)).ConfigureAwait(false);
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Vocabulary)).ConfigureAwait(false);
                    initialized = true;
                }                
            }
        }


        public Task<List<Modul>> GetModulesAsync()
        {
            return Database.Table<Modul>().ToListAsync();
        }
        public Task<List<Vocabulary>> GetVocablesAsync()
        {
            return Database.Table<Vocabulary>().ToListAsync();
        }
        public Task<Modul> GetModuleByIDAsync(int ModulID)
        {
            return Database.GetAsync<Modul>(ModulID);
        }

        public Task<List<Modul>> GetItemsNotDoneAsync()
        {
            // SQL queries are also possible    
            return Database.QueryAsync<Modul>("SELECT * FROM Modul");
        }

        public Task<Modul> GetItemAsync(int id)
        {
            return Database.Table<Modul>().Where(i => i.ModulID == id).FirstOrDefaultAsync();
        }
        public Task<List<Vocabulary>> GetVocableAsync(int ModulID)
        {
            return Database.Table<Vocabulary>().Where(i => i.ModulID == ModulID).ToListAsync();
        }

        public Task<int> SaveItemAsync(Modul item)
        {
            if (item.ModulID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(Modul item)
        {
            return Database.DeleteAsync(item);
        }

        public  Task InsertOrUpdateOrDeleteVocablesAsync(List<Vocabulary> vocablesToSave)
        {
            return Task.Run(() => {
               
                foreach (Vocabulary vocable in vocablesToSave)
                {
                    if (vocable.VocableID != 0 && vocable.English != "" && vocable.German != "")
                    {
                        Database.UpdateAsync(vocable);
                    }
                    else if (vocable.VocableID != 0 && vocable.English == "" && vocable.German == "")
                    {
                        Database.DeleteAsync<Vocabulary>(vocable.VocableID);
                    }
                    else
                    {
                        Database.InsertAsync(vocable);
                    }
                }
            });


        }
        public async Task UpdateVocable(Vocabulary vocable)
        {
            await Database.UpdateAsync(vocable);
        }

        //todo: Inplement InsertVocables

        public async Task InsertModules(List<Modul> modulsToSafe)
        {
            List<Task<int>> tasks = new List<Task<int>>();
            foreach (Modul modul in modulsToSafe)
            {
                var rowsAffected = Database.UpdateAsync(modul).Result;
                if (rowsAffected == 0)
                {
                    tasks.Add(Database.InsertAsync(modul));
                }
            }
            await Task.WhenAll(tasks);
            //todo: Inplement InsertModules
        }
        public async Task DeleteVocablesByFKModukID(int ModulID)
        {
            await Database.QueryAsync<Vocabulary>($"DELETE FROM Vocabulary WHERE ModulID={ModulID}");
        }
        public async Task DeleteModul(Modul modulToDelete)
        {
            await Database.DeleteAsync<Modul>(modulToDelete.ModulID);
        }
    }
}





