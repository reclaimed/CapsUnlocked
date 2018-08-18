﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;


public static class SoundGenerator
{


    public static void PlayBeep(UInt16 frequency, int msDuration, UInt16 volume = 16383)
    {
        var mStrm = new MemoryStream();
        BinaryWriter writer = new BinaryWriter(mStrm);

        const double TAU = 2 * Math.PI;
        int formatChunkSize = 16;
        int headerSize = 8;
        short formatType = 1;
        short tracks = 1;
        int samplesPerSecond = 44100;
        short bitsPerSample = 16;
        short frameSize = (short)(tracks * ((bitsPerSample + 7) / 8));
        int bytesPerSecond = samplesPerSecond * frameSize;
        int waveSize = 4;
        int samples = (int)((decimal)samplesPerSecond * msDuration / 1000);
        int dataChunkSize = samples * frameSize;
        int fileSize = waveSize + headerSize + formatChunkSize + headerSize + dataChunkSize;
        // var encoding = new System.Text.UTF8Encoding();
        writer.Write(0x46464952); // = encoding.GetBytes("RIFF")
        writer.Write(fileSize);
        writer.Write(0x45564157); // = encoding.GetBytes("WAVE")
        writer.Write(0x20746D66); // = encoding.GetBytes("fmt ")
        writer.Write(formatChunkSize);
        writer.Write(formatType);
        writer.Write(tracks);
        writer.Write(samplesPerSecond);
        writer.Write(bytesPerSecond);
        writer.Write(frameSize);
        writer.Write(bitsPerSample);
        writer.Write(0x61746164); // = encoding.GetBytes("data")
        writer.Write(dataChunkSize);
        {
            double theta = frequency * TAU / (double)samplesPerSecond;
            // 'volume' is UInt16 with range 0 thru Uint16.MaxValue ( = 65 535)
            // we need 'amp' to have the range of 0 thru Int16.MaxValue ( = 32 767)
            double amp = volume >> 2; // so we simply set amp = volume / 2
            for (int step = 0; step < samples; step++)
            {
                short s = (short)(amp * Math.Sin(theta * (double)step));
                writer.Write(s);
            }
        }

        mStrm.Seek(0, SeekOrigin.Begin);
        new System.Media.SoundPlayer(mStrm).Play();
        writer.Close();
        mStrm.Close();
    } // public static void PlayBeep(UInt16 frequency, int msDuration, UInt16 volume = 16383)

}