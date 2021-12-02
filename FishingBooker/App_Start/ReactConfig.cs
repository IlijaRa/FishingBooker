using JavaScriptEngineSwitcher.V8;
using JavaScriptEngineSwitcher.Core;
using React;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(FishingBooker.ReactConfig), "Configure")]

namespace FishingBooker
{
	public static class ReactConfig
	{
		public static void Configure()
		{
			JsEngineSwitcher.Current.DefaultEngineName = V8JsEngine.EngineName;
			JsEngineSwitcher.Current.EngineFactories.AddV8();
		}
	}
}