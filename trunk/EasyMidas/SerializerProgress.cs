using System;
using System.IO;
using System.ComponentModel;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace SerializerProgress
{
    /// <summary>
    /// 此代码参考网址：http://msdn.microsoft.com/zh-cn/magazine/cc163515.aspx
    /// 《反序列化进度和其他问题》
    /// </summary>
    class Utilities
    {
        /// <summary>
        /// 带进度跟踪的反序列化
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="stream">文件流</param>
        /// <param name="callback">进度改变代表</param>
        /// <returns>返回相应的对像类型</returns>
        public static T Deserialize<T>(Stream stream, ProgressChangedEventHandler callback)
        {
            if (stream == null) throw new ArgumentNullException("stream");

            using (ReadProgressStream cs = new ReadProgressStream(stream))
            {
                cs.ProgressChanged += callback;

                const int defaultBufferSize = 4096;
                int onePercentSize = (int)Math.Ceiling(stream.Length / 100.0);

                using (BufferedStream bs = new BufferedStream(cs,
                    onePercentSize > defaultBufferSize ? defaultBufferSize : onePercentSize))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    return (T)formatter.Deserialize(bs);
                }
            }
        }
    }

    public class ReadProgressStream : ContainerStream
    {
        private int _lastProgress = 0;

        public ReadProgressStream(Stream stream)
            : base(stream)
        {
            if (stream.Length <= 0 || !stream.CanRead) throw new ArgumentException("stream");
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int amountRead = base.Read(buffer, offset, count);
            if (ProgressChanged != null)
            {
                int newProgress = (int)(Position * 100.0 / Length);
                if (newProgress > _lastProgress)
                {
                    _lastProgress = newProgress;
                    ProgressChanged(this, new ProgressChangedEventArgs(_lastProgress, null));
                }
            }
            return amountRead;
        }

        public event ProgressChangedEventHandler ProgressChanged;
    }

    /// <summary>
    /// 抽象类
    /// </summary>
    public abstract class ContainerStream : Stream
    {
        private Stream _stream;

        protected ContainerStream(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException("stream");
            _stream = stream;
        }

        protected Stream ContainedStream { get { return _stream; } }

        public override bool CanRead { get { return _stream.CanRead; } }

        public override bool CanSeek { get { return _stream.CanSeek; } }

        public override bool CanWrite { get { return _stream.CanWrite; } }

        public override void Flush() { _stream.Flush(); }

        public override long Length { get { return _stream.Length; } }

        public override long Position
        {
            get { return _stream.Position; }
            set { _stream.Position = value; }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _stream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _stream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _stream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _stream.Write(buffer, offset, count);
        }
    }
}