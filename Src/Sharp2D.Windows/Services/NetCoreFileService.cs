// Decompiled with JetBrains decompiler
// Type: Sharp2D.Windows.Services.NetCoreFileService
// Assembly: Sharp2D.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C63E555-7333-49A8-BC75-2195F04CFD5D
// Assembly location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Windows.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\ChasingGhosts\Sharp2D.Windows.xml

using Sharp2D.Engine.Common;
using Sharp2D.Engine.Infrastructure;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Sharp2D.Windows.Services
{
  /// <summary>
  /// .NET Core File Service, delegating calls to the File static.
  /// </summary>
  public class NetCoreFileService : IFileService
  {
    /// <summary>Determines if the file exists.</summary>
    /// <param name="path">The path.</param>
    /// <returns></returns>
    public bool Exists(string path)
    {
      return File.Exists(this.MapPath(new string[1]{ path }));
    }

    /// <summary>Maps the path to be relative to the content root.</summary>
    /// <param name="paths">The paths.</param>
    /// <returns></returns>
    public string MapPath(params string[] paths)
    {
      List<string> stringList = new List<string>()
      {
        Sharp2DApplication.ContentRoot
      };
      stringList.AddRange((IEnumerable<string>) paths);
      return Path.Combine(stringList.ToArray());
    }

    /// <summary>Reads all text from the specified file.</summary>
    /// <param name="filePath">The file path.</param>
    /// <returns></returns>
    public string ReadAllText(string filePath)
    {
      return File.ReadAllText(this.MapPath(new string[1]
      {
        filePath
      }));
    }

    /// <summary>Writes all text to the specified file.</summary>
    /// <param name="filePath">The file path.</param>
    /// <param name="content">The content.</param>
    public void WriteAllText(string filePath, string content)
    {
      File.WriteAllText(this.MapPath(new string[1]
      {
        filePath
      }), content);
    }

    /// <summary>Opens a read stream to the specified file.</summary>
    /// <param name="assetName">Name of the asset.</param>
    /// <returns></returns>
    public Stream ReadStream(string assetName)
    {
      return (Stream) File.OpenRead(this.MapPath(new string[1]
      {
        assetName
      }));
    }
  }
}
