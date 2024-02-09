// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Infrastructure.Serialization.ISerializer
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

#nullable disable
namespace Sharp2D.Engine.Infrastructure.Serialization
{
  /// <summary>Serializer contract.</summary>
  public interface ISerializer
  {
    /// <summary>The deserialize.</summary>
    /// <param name="content">The content.</param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// The <see cref="!:T" />.
    /// </returns>
    T Deserialize<T>(string content);

    /// <summary>The deserialize from file.</summary>
    /// <param name="file">The file.</param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// The <see cref="!:T" />.
    /// </returns>
    T DeserializeFromFile<T>(string file);

    /// <summary>The serialize.</summary>
    /// <param name="obj">The obj.</param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// The <see cref="T:System.String" />.
    /// </returns>
    string Serialize<T>(T obj);

    /// <summary>The serialize to file.</summary>
    /// <param name="obj">The obj.</param>
    /// <param name="file">The file.</param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// The <see cref="T:System.String" />.
    /// </returns>
    string SerializeToFile<T>(T obj, string file);
  }
}
