using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TagCloudBuilder.WordsConverter
{
	public class WordReaderFromFile	: IWordReader
	{
		private readonly string _filename;

		public WordReaderFromFile(string filename)
		{
			_filename = filename;
		}

		public Result<IEnumerable<string>> ReadWords()
		{
			return Result.Of(() => File.ReadLines(_filename, Encoding.Default), $"Нет файла {_filename}");
		}
	}
}
