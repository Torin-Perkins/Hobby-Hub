using Firebase.Database;
using Firebase.Database.Query;
using HobbyHub.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HobbyHub
{
    public class FirebaseHelper {
        public static FirebaseClient firebase = new FirebaseClient("https://hobbyhub-e6f54.firebaseio.com/");
        public static async Task<List<Person>> GetAllUser()
        {
            try
            {
                var userlist = (await firebase
                .Child("Person")
                .OnceAsync<Person>()).Select(item =>
                new Person
                {
                    Email = item.Object.Email,
                    Password = item.Object.Password
                }).ToList();
                return userlist;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        //Read     
        public static async Task<Person> GetUser(string email)
        {
            try
            {
                var allPerson = await GetAllUser();
                await firebase
                .Child("Person")
                .OnceAsync<Person>();
                return allPerson.Where(a => a.Email == email).FirstOrDefault();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        //Inser a user    
        public static async Task<bool> AddUser(string email, string password)
        {
            try
            {


                await firebase
                .Child("Person")
                .PostAsync(new Person() { Email = email, Password = password });
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }

        //Update     
        public static async Task<bool> UpdateUser(string email, string password)
        {
            try
            {


                var toUpdateUser = (await firebase
                .Child("Person")
                .OnceAsync<Person>()).Where(a => a.Object.Email == email).FirstOrDefault();
                await firebase
                .Child("Person")
                .Child(toUpdateUser.Key)
                .PutAsync(new Person() { Email = email, Password = password });
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }

        //Delete User    
        public static async Task<bool> DeleteUser(string email)
        {
            try
            {


                var toDeletePerson = (await firebase
                .Child("Person")
                .OnceAsync<Person>()).Where(a => a.Object.Email == email).FirstOrDefault();
                await firebase.Child("Person").Child(toDeletePerson.Key).DeleteAsync();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }

        /*
                FirebaseClient firebase = new FirebaseClient("https://hobbyhub-e6f54.firebaseio.com/");
                public async Task<List<Person>> GetAllPersons()
                {
                    return (await firebase.Child("Persons").OnceAsync<Person>()).Select(item => new Person
                    {
                        Username = item.Object.Username,
                        PersonId = item.Object.PersonId
                    }).ToList();
                }

                public async Task AddPerson(int personId, string name)
                {

                    await firebase
                      .Child("Persons")
                      .PostAsync(new Person() { PersonId = personId, Username = name });
                }
                public async Task<Person> GetPerson(int personId)
                {
                    var allPersons = await GetAllPersons();
                    await firebase
                      .Child("Persons")
                      .OnceAsync<Person>();
                    return allPersons.Where(a => a.PersonId == personId).FirstOrDefault();
                }
                public async Task UpdatePerson(int personId, string name)
                {
                    var toUpdatePerson = (await firebase
                      .Child("Persons")
                      .OnceAsync<Person>()).Where(a => a.Object.PersonId == personId).FirstOrDefault();

                    await firebase
                      .Child("Persons")
                      .Child(toUpdatePerson.Key)
                      .PutAsync(new Person() { PersonId = personId, Username = name });
                }
                public async Task DeletePerson(int personId)
                {
                    var toDeletePerson = (await firebase
                      .Child("Persons")
                      .OnceAsync<Person>()).Where(a => a.Object.PersonId == personId).FirstOrDefault();
                    await firebase.Child("Persons").Child(toDeletePerson.Key).DeleteAsync();

                }
                */
    }
}
