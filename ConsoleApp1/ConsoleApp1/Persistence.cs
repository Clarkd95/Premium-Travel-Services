using System;
using System.Collections.Generic;
using System.Text;


namespace ProjectIteration3
{
    //public enum FileType
    //{
    //    XML,
    //    JSON
    //}

    //public static class ReaderFactory<T> where T : class, new()
    //{
    //    public static IReader<T> Get(FileType strategy)
    //    {
    //        switch (strategy)
    //        {
    //            case FileType.JSON: return new JSONReader<T>();
    //            case FileType.XML: return new XMLReader<T>();
    //            default: throw new KeyNotFoundException($"Unknown strategy {strategy}");
    //        }
    //    }
    //}

    //public interface IReader<T> where T : class, new()
    //{
    //    T Read(string filename);
    //}

    //public class JSONReader<T> : IReader<T> where T : class, new()
    //{
    //    public T Read(string filename)
    //    {
    //        T toReturn = new T();
    //        Console.WriteLine($"Deserialized JSON in {filename} to type T={toReturn.GetType().Name}");
    //        return toReturn;
    //    }
    //}

    //public class XMLReader<T> : IReader<T> where T : class, new()
    //{
    //    public T Read(string filename)
    //    {
    //        T toReturn = new T();
    //        Console.WriteLine($"Deserialized XML in {filename} to type T={toReturn.GetType().Name}");
    //        return toReturn;
    //    }
    //}

    //public static class WriterFactory
    //{
    //    public static IWriter Get(FileType strategy)
    //    {
    //        switch (strategy)
    //        {
    //            case FileType.JSON: return new JSONWriter();
    //            case FileType.XML: return new XMLWriter();
    //            default: throw new KeyNotFoundException($"Unknown strategy {strategy}");
    //        }
    //    }
    //}
    //public interface IWriter
    //{
    //    void Write(object toWrite);
    //}



    //public class XMLWriter : IWriter
    //{


    //    public void Write(object toWrite)
    //    {
    //        //string globalTrips = son.JsonWriter;
    //    }
    //}

    //public class JSONWriter : IWriter
    //{

    //    public void Write(object toWrite)
    //    {
    //    //
    //    }
    //}

    
}
