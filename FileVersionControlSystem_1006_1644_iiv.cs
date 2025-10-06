// 代码生成时间: 2025-10-06 16:44:48
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileVersionControlSystem
{
    /// <summary>
    /// Represents a file version control system that allows tracking and reverting file versions.
    /// </summary>
    public class FileVersionControl
    {
        private readonly string _repositoryPath;
        private readonly Dictionary<string, List<string>> _fileVersions = new Dictionary<string, List<string>>();

        /// <summary>
        /// Initializes a new instance of the FileVersionControl class.
        /// </summary>
        /// <param name="repositoryPath">The path to the file repository.</param>
        public FileVersionControl(string repositoryPath)
        {
            _repositoryPath = repositoryPath;
        }

        /// <summary>
        /// Adds a new version of the file to the repository.
        /// </summary>
        /// <param name="filePath">The path of the file to version.</param>
        public async Task AddFileVersionAsync(string filePath)
        {
            var fileName = Path.GetFileName(filePath);
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("The file does not exist.", filePath);
            }

            if (!_fileVersions.ContainsKey(fileName))
            {
                _fileVersions[fileName] = new List<string>();
            }

            var version = await File.ReadAllTextAsync(filePath);
            _fileVersions[fileName].Add(version);
        }

        /// <summary>
        /// Reverts to a specific version of the file.
        /// </summary>
        /// <param name="filePath">The path of the file to revert.</param>
        /// <param name="versionNumber">The version number to revert to.</param>
        public async Task RevertToVersionAsync(string filePath, int versionNumber)
        {
            var fileName = Path.GetFileName(filePath);
            if (!_fileVersions.ContainsKey(fileName) || versionNumber < 0 || versionNumber >= _fileVersions[fileName].Count)
            {
                throw new InvalidOperationException("Invalid version number for the file.");
            }

            var version = _fileVersions[fileName][versionNumber];
            await File.WriteAllTextAsync(filePath, version);
        }

        /// <summary>
        /// Lists all versions of a file.
        /// </summary>
        /// <param name="filePath">The path of the file to list versions for.</param>
        /// <returns>A list of file versions.</returns>
        public List<string> ListFileVersions(string filePath)
        {
            var fileName = Path.GetFileName(filePath);
            if (!_fileVersions.ContainsKey(fileName))
            {
                return new List<string>();
            }

            return _fileVersions[fileName];
        }
    }
}
