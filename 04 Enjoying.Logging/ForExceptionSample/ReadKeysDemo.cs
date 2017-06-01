using System;

namespace ForExceptionSample
{
    public class ReadKeysDemo
    {
        public void ReadKeys()
        {
            try
            {
                for (int i = 0; i < 100; i++)
                {
                    try
                    {
                        throw new ArgumentNullException("key", $"Key {i} not found");
                    }
                    catch (Exception){}
                }
            }
            catch (Exception ex){}
            
        }
    }
}
