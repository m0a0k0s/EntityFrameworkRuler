﻿using System.Reflection;
using EdmxRuler.Applicator;
using EdmxRuler.Extensions;
using EdmxRuler.Generator;

namespace EdmxRuler;

internal static class Program {
    private static async Task<int> Main(string[] args) {
        try {
            if (args.IsNullOrEmpty() || args[0].IsNullOrWhiteSpace()) return await ShowHelpInfo();

            var option = GetSwitchArg(args[0]);
            switch (option) {
                case 'g': {
                    // generate rules
                    if (!GeneratorArgHelper.TryParseArgs(args.Skip(1).ToArray(), out var edmxPath, out var projectBasePath))
                        return await ShowHelpInfo();

                    var start = DateTimeExtensions.GetTime();
                    var edmxProcessor = new EdmxRuleGenerator(edmxPath);
                    var rules = edmxProcessor.TryGenerateRules();
                    await edmxProcessor.TrySaveRules(projectBasePath);
                    var elapsed = DateTimeExtensions.GetTime() - start;
                    if (edmxProcessor.Errors.Count == 0) {
                        await Console.Out
                            .WriteLineAsync($"Successfully generated {rules.Count} rule files in {elapsed}ms")
                            .ConfigureAwait(false);
                        return 0;
                    }

                    foreach (var error in edmxProcessor.Errors)
                        await Console.Out.WriteLineAsync($"Edmx generator error encountered: {error}")
                            .ConfigureAwait(false);

                    return edmxProcessor.Errors.Count;
                }
                case 'a': {
                    // apply rules
                    if (!ApplicatorArgHelper.TryParseArgs(args.Skip(1).ToArray(), out var projectBasePath))
                        return await ShowHelpInfo();

                    var errors = await RoslynRuleApplicator.ApplyRulesToPath(projectBasePath);

                    foreach (var error in errors)
                        await Console.Out.WriteLineAsync($"Rule applicator error encountered: {error}")
                            .ConfigureAwait(false);

                    return errors.Count;
                }
                default:
                    return await ShowHelpInfo();
            }
        } catch (Exception ex) {
            await Console.Out.WriteLineAsync($"Unexpected error: {ex.Message}").ConfigureAwait(false);
            return 1;
        }
    }

    private static char GetSwitchArg(string arg) {
        if (arg.IsNullOrWhiteSpace() || arg.Length < 2) return default;
        var firstChar = arg[0];
        return firstChar is '-' or '/' ? char.ToLower(arg[1]) : default;
    }

    private static async Task<int> ShowHelpInfo() {
        var versionString = Assembly.GetEntryAssembly()?
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
            .InformationalVersion
            .ToString();
        await Console.Out.WriteLineAsync($"EdmxRuler v{versionString}");
        await Console.Out.WriteLineAsync("-----------------------");

        await Console.Out.WriteLineAsync("\nRule Generation Usage:");
        await Console.Out.WriteLineAsync("  EdmxRuler -g <edmxfilepath> <efCoreProjectBasePath>");
        await Console.Out.WriteLineAsync("  EdmxRuler -g <pathContainingEdmxAndCsProj>");
        await Console.Out.WriteLineAsync("  EdmxRuler -g . (assuming current directory contains edmx and csproj)");

        await Console.Out.WriteLineAsync("\nRule Applicator Usage:");
        await Console.Out.WriteLineAsync("  EdmxRuler -a <pathContainingRulesAndCsProj>");
        await Console.Out.WriteLineAsync("  EdmxRuler -a . (assuming current directory contains edmx and csproj)");

        return 1;
    }
}