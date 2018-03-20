using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;
using System.IO;
using System.Speech.AudioFormat;

namespace Common.Extensions
{
      /// <summary>
      /// Windows语音合成 助手
      /// 注意系统默认配置不支持英文发音
      /// </summary>
      public static class EasyWindowsSpeech
      {
            /// <summary>
            /// 通过Windows默认音频输出设备朗读文本
            /// </summary>
            /// <param name="textToSpeak">待朗读的文本</param>
            public static void Speak(string textToSpeak)
            {
                  SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();
                  speechSynthesizer.Speak(textToSpeak);
            }

            /// <summary>
            /// 将文本合成为音频流 不工作？
            /// </summary>
            /// <param name="textToSpeak">待合成音频</param>
            public static Stream SpeakToWaveStream(string textToSpeak)
            {
                  MemoryStream stream = new MemoryStream();
                  SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();
                  speechSynthesizer.SetOutputToWaveStream(stream);
                  speechSynthesizer.Speak(textToSpeak);
                  speechSynthesizer.SetOutputToNull();
                  stream.Position = 0;
                  return stream;
            }

            /// <summary>
            /// 将文本合成为音频文件（mp3或wav）
            /// </summary>
            /// <param name="path">文件完整路径</param>
            /// <param name="textToSpeak">待合成文本</param>
            public static void SpeakToFile(string path, string textToSpeak)
            {
                  SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();
                  speechSynthesizer.SetOutputToWaveFile(path);
                  speechSynthesizer.Speak(textToSpeak);
            }
      }
}
