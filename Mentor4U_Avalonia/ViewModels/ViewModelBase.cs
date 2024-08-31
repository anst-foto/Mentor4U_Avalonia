// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using ReactiveUI;

namespace Mentor4U_Avalonia.ViewModels;

public abstract class ViewModelBase : ReactiveObject
{
    protected ILogger Logger { get; init; }
    protected ILoggerFactory loggerFactory;
    protected ViewModelBase()
    {
        loggerFactory = LoggerFactory.Create(builder => builder.AddNLog());
    }
}
