using DbUp.Engine.Output;
using Serilog;

namespace CanonnApi.Database
{
	internal class UpgradeLoggerSerilog : IUpgradeLog
	{
		public ILogger Logger { get; }

		public UpgradeLoggerSerilog(ILogger logger)
		{
			Logger = logger;
		}

		public void WriteInformation(string format, params object[] args)
		{
			Logger.Information(format, args);
		}

		public void WriteError(string format, params object[] args)
		{
			Logger.Error(format, args);
		}

		public void WriteWarning(string format, params object[] args)
		{
			Logger.Warning(format, args);
		}
	}
}
