using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
namespace Demo.Services
{
   public class PreferenceStorage:IPreferenceStorage
   {
       public void Set(string key, string value) =>
           Preferences.Set(key, value);

       public void Set(string key, int value) => 
           Preferences.Set(key, value);


       public string Get(string key, string defaultValue) =>
           Preferences.Get(key,defaultValue);


       public int Get(string key, int defaultValue) =>
           Preferences.Get(key, defaultValue);

   }
}
