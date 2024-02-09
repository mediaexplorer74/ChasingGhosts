// Decompiled with JetBrains decompiler
// Type: Sharp2D.Engine.Infrastructure.IFileService
// Assembly: Sharp2D.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4657E4E-8156-418C-A29E-C786A72AEE89
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Engine.xml

using System.IO;

#nullable disable
namespace Sharp2D.Engine.Infrastructure
{
  /// <summary>
  ///     File service interface - should be implemented on each platform.
  /// </summary>
  public interface IFileService
  {
    /// <summary>Determines if the file exists.</summary>
    /// <param name="path">The path.</param>
    /// <returns>
    /// The <see cref="T:System.Boolean" />.
    /// </returns>
    bool Exists(string path);

    /// <summary>Maps the path to be relative to the content root.</summary>
    /// <param name="paths">The paths.</param>
    /// <returns>
    /// The <see cref="T:System.String" />.
    /// </returns>
    string MapPath(params string[] paths);

    /// <summary>Reads all text from the specified file.</summary>
    /// <param name="filePath">The file path.</param>
    /// <returns>
    /// The <see cref="T:System.String" />.
    /// </returns>
    string ReadAllText(string filePath);

    /// <summary>Opens a read stream to the specified file.</summary>
    /// <param name="assetName">Name of the asset.</param>
    /// <returns>
    /// The <see cref="T:System.IO.Stream" />.
    /// </returns>
    Stream ReadStream(string assetName);

    /// <summary>Writes all text to the specified file.</summary>
    /// <param name="filePath">The file path.</param>
    /// <param name="content">The content.</param>
    void WriteAllText(string filePath, string content);
  }
}
