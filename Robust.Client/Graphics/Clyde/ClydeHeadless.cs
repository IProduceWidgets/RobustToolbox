﻿using System;
using System.IO;
using Robust.Client.Audio;
using Robust.Client.Graphics.Shaders;
using Robust.Client.Input;
using Robust.Client.Interfaces.Graphics;
using Robust.Client.Interfaces.Graphics.ClientEye;
using Robust.Shared.Maths;
using Robust.Shared.Timing;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Color = Robust.Shared.Maths.Color;

namespace Robust.Client.Graphics.Clyde
{
    /// <summary>
    ///     Hey look, it's Clyde's evil twin brother!
    /// </summary>
    internal sealed class ClydeHeadless : ClydeBase, IClydeInternal, IClydeAudio
    {
        // Would it make sense to report a fake resolution like 720p here so code doesn't break? idk.
        public override Vector2i ScreenSize { get; } = (1280, 720);

        public ShaderInstance InstanceShader(ClydeHandle handle)
        {
            return new DummyShaderInstance();
        }

        public Vector2 MouseScreenPosition => ScreenSize / 2;
        public IClydeDebugInfo DebugInfo { get; } = new DummyDebugInfo();
        public IClydeDebugStats DebugStats { get; } = new DummyDebugStats();

        public Texture GetStockTexture(ClydeStockTexture stockTexture)
        {
            return new DummyTexture((1, 1));
        }

        public ClydeDebugLayers DebugLayers { get; set; }

        public string GetKeyName(Keyboard.Key key) => string.Empty;
        public string GetKeyNameScanCode(int scanCode) => string.Empty;
        public int GetKeyScanCode(Keyboard.Key key) => default;
        public void Shutdown()
        {
            // Nada.
        }

        public override void SetWindowTitle(string title)
        {
            // Nada.
        }

        public override bool Initialize()
        {
            return true;
        }

        public override event Action<WindowResizedEventArgs> OnWindowResized
        {
            add {}
            remove {}
        }

        public void Render()
        {
            // Nada.
        }

        public void FrameProcess(FrameEventArgs eventArgs)
        {
            // Nada.
        }

        public void ProcessInput(FrameEventArgs frameEventArgs)
        {
            // Nada.
        }

        public Texture LoadTextureFromPNGStream(Stream stream, string? name = null,
            TextureLoadParameters? loadParams = null)
        {
            using (var image = Image.Load<Rgba32>(stream))
            {
                return LoadTextureFromImage(image, name, loadParams);
            }
        }

        public Texture LoadTextureFromImage<T>(Image<T> image, string? name = null,
            TextureLoadParameters? loadParams = null) where T : unmanaged, IPixel<T>
        {
            return new DummyTexture((image.Width, image.Height));
        }

        public IRenderTarget CreateRenderTarget(Vector2i size, RenderTargetFormatParameters format,
            TextureSampleParameters? sampleParameters = null, string? name = null)
        {
            return new DummyRenderTarget(size, new DummyTexture(size));
        }

        public void CalcWorldProjectionMatrix(out Matrix3 projMatrix)
        {
            projMatrix = Matrix3.Identity;
        }

        public ICursor GetStandardCursor(StandardCursorShape shape)
        {
            return new DummyCursor();
        }

        public ICursor CreateCursor(Image<Rgba32> image, Vector2i hotSpot)
        {
            return new DummyCursor();
        }

        public void SetCursor(ICursor? cursor)
        {
            // Nada.
        }

        public void Screenshot(ScreenshotType type, Action<Image<Rgb24>> callback)
        {
            callback(new Image<Rgb24>(ScreenSize.X, ScreenSize.Y));
        }

        public IClydeViewport CreateViewport()
        {
            return new Viewport();
        }

        public ClydeHandle LoadShader(ParsedShader shader, string? name = null)
        {
            return default;
        }

        public void ReloadShader(ClydeHandle handle, ParsedShader newShader)
        {
            // Nada.
        }

        public void Ready()
        {
            // Nada.
        }

        public AudioStream LoadAudioOggVorbis(Stream stream, string? name = null)
        {
            // TODO: Might wanna actually load this so the length gets reported correctly.
            return new AudioStream(default, default, 1, name);
        }

        public AudioStream LoadAudioWav(Stream stream, string? name = null)
        {
            // TODO: Might wanna actually load this so the length gets reported correctly.
            return new AudioStream(default, default, 1, name);
        }

        public IClydeAudioSource CreateAudioSource(AudioStream stream)
        {
            return DummyAudioSource.Instance;
        }

        public IClydeBufferedAudioSource CreateBufferedAudioSource(int buffers, bool floatAudio=false)
        {
            return DummyBufferedAudioSource.Instance;
        }

        public string GetText()
        {
            return string.Empty;
        }

        public void SetText(string text)
        {
            // Nada.
        }

        private class DummyCursor : ICursor
        {
            public void Dispose()
            {
                // Nada.
            }
        }

        private class DummyAudioSource : IClydeAudioSource
        {
            public static DummyAudioSource Instance { get; } = new DummyAudioSource();

            public bool IsPlaying => default;
            public bool IsLooping { get; set; }

            public void Dispose()
            {
                // Nada.
            }

            public void StartPlaying()
            {
                // Nada.
            }

            public void StopPlaying()
            {
                // Nada.
            }

            public bool SetPosition(Vector2 position)
            {
                return true;
            }

            public void SetPitch(float pitch)
            {
                // Nada.
            }

            public void SetGlobal()
            {
                // Nada.
            }

            public void SetLooping()
            {
                // Nada.
            }

            public void SetVolume(float decibels)
            {
                // Nada.
            }

            public void SetOcclusion(float blocks)
            {
                // Nada.
            }

            public void SetPlaybackPosition(float seconds)
            {
                // Nada.
            }
        }

        private sealed class DummyBufferedAudioSource : DummyAudioSource, IClydeBufferedAudioSource
        {
            public new static DummyBufferedAudioSource Instance { get; } = new DummyBufferedAudioSource();
            public int SampleRate { get; set; } = 0;

            public void WriteBuffer(int handle, ReadOnlySpan<ushort> data)
            {
                // Nada.
            }

            public void WriteBuffer(int handle, ReadOnlySpan<float> data)
            {
                // Nada.
            }

            public void QueueBuffer(int handle)
            {
                // Nada.
            }

            public void QueueBuffers(ReadOnlySpan<int> handles)
            {
                // Nada.
            }

            public void EmptyBuffers()
            {
                // Nada.
            }

            public void GetBuffersProcessed(Span<int> handles)
            {
                // Nada.
            }

            public int GetNumberOfBuffersProcessed()
            {
                return 0;
            }
        }

        private sealed class DummyTexture : OwnedTexture
        {
            public override void Delete()
            {
                // Hey that was easy.
            }

            public DummyTexture(Vector2i size) : base(size)
            {
            }
        }

        private sealed class DummyShaderInstance : ShaderInstance
        {
            private protected override ShaderInstance DuplicateImpl()
            {
                return new DummyShaderInstance();
            }

            private protected override void SetParameterImpl(string name, float value)
            {
            }

            private protected override void SetParameterImpl(string name, Vector2 value)
            {
            }

            private protected override void SetParameterImpl(string name, Vector3 value)
            {
            }

            private protected override void SetParameterImpl(string name, Vector4 value)
            {
            }

            private protected override void SetParameterImpl(string name, Color value)
            {
            }

            private protected override void SetParameterImpl(string name, int value)
            {
            }

            private protected override void SetParameterImpl(string name, Vector2i value)
            {
            }

            private protected override void SetParameterImpl(string name, bool value)
            {
            }

            private protected override void SetParameterImpl(string name, in Matrix3 value)
            {
            }

            private protected override void SetParameterImpl(string name, in Matrix4 value)
            {
            }

            private protected override void SetParameterImpl(string name, Texture value)
            {
            }

            private protected override void SetStencilOpImpl(StencilOp op)
            {
            }

            private protected override void SetStencilFuncImpl(StencilFunc func)
            {
            }

            private protected override void SetStencilTestEnabledImpl(bool enabled)
            {
            }

            private protected override void SetStencilRefImpl(int @ref)
            {
            }

            private protected override void SetStencilWriteMaskImpl(int mask)
            {
            }

            private protected override void SetStencilReadMaskRefImpl(int mask)
            {
            }
        }

        private sealed class DummyRenderTarget : IRenderTarget
        {
            public DummyRenderTarget(Vector2i size, Texture texture)
            {
                Size = size;
                Texture = texture;
            }

            public Vector2i Size { get; }
            public Texture Texture { get; }

            public void Delete()
            {
            }
        }

        private sealed class DummyDebugStats : IClydeDebugStats
        {
            public int LastGLDrawCalls => 0;
            public int LastClydeDrawCalls => 0;
            public int LastBatches => 0;
            public (int vertices, int indices) LargestBatchSize => (0, 0);
        }

        private sealed class DummyDebugInfo : IClydeDebugInfo
        {
            public Version OpenGLVersion { get; } = new Version(3, 3);
            public Version MinimumVersion { get; } = new Version(3, 3);
            public string Renderer => "ClydeHeadless";
            public string Vendor => "Space Wizards Federation";
            public string VersionString { get; } = $"3.3.0 WIZARDS {typeof(DummyDebugInfo).Assembly.GetName().Version}";
        }

        private sealed class Viewport : IClydeViewport
        {
            public void Dispose()
            {
            }

            public IRenderTarget RenderTarget { get; } = new DummyRenderTarget(Vector2i.One, new DummyTexture(Vector2i.One));

            public IEye Eye { get; set; }
            public void Resize(Vector2i newSize)
            {
            }
        }
    }
}
