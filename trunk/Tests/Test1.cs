﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoundTouchNet;

namespace Tests
{
  [TestClass]
  public class Test1
  {
    [TestMethod]
    public void TestMethod1()
    {
      using (SoundStretcher stretcher = new SoundStretcher(16000, 1))
      {

        Assert.IsNotNull(stretcher);
        Assert.IsTrue(stretcher.CanWrite);
        Assert.IsFalse(stretcher.CanRead);
        Assert.AreEqual(stretcher.AvaiableSamples, 0);

        stretcher.Tempo = 2;

        Random rnd = new Random();

        float[] samplesIn = new float[16000];

        for (int i = 0; i < samplesIn.Length; i++)
          samplesIn[i] = (float)rnd.NextDouble();

        stretcher.PutSamples(samplesIn, samplesIn.Length);

        stretcher.Flush();
        Assert.IsTrue(stretcher.CanRead);

        float[] samplesOut = new float[stretcher.AvaiableSamples];
        stretcher.ReceiveSamples(samplesOut, samplesOut.Length);

        Assert.AreEqual(stretcher.AvaiableSamples, 0);
        Assert.IsFalse(stretcher.CanRead);
        stretcher.Clear();
      }

    }


  }
}