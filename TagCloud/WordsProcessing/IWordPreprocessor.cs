using TagCloud.Common;

namespace TagCloud.WordsProcessing;

internal interface IWordPreprocessor
{
	Result<string[]> Process(string[] strings);
}