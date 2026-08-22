using SPTarkov.Common.Models.Logging;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Extensions;
using SPTarkov.Server.Core.Models.Spt.Mod;
using SPTarkov.Server.Core.Models.Spt.Tables;

namespace TwoSlotBigMags;

public sealed record ModMetadata : IModMetadata
{
    public string ModGuid { get; init; } = "com.rangedace.twoslotbigmags";
    public string Name { get; init; } = "Two Slot Big Mags";
    public string Author { get; init; } = "rangedace";
    public List<string>? Contributors { get; init; }
    public SemanticVersioning.Version Version { get; init; } = new("1.0.0");
    public SemanticVersioning.Range SptVersion { get; init; } = new("~4.1.0");
    public List<string>? Incompatibilities { get; init; }
    public Dictionary<string, SemanticVersioning.Range>? ModDependencies { get; init; }
    public string? Url { get; init; } = "https://github.com/rangedace/TwoSlotBigMags";
    public string License { get; init; } = "MIT";
    public bool HasPrepatcher { get; init; }
}

// Run after SPT and ordinary database mods so their 40-round magazines are included too.
[Injectable(TypePriority = OnLoadOrder.PostLoad + 1000)]
public sealed class TwoSlotBigMagsMod(
    TemplateTable templateTable,
    ISptLogger<TwoSlotBigMagsMod> logger) : IOnLoad
{
    private const int TargetCapacity = 40;
    private const int OriginalHeight = 3;
    private const int TargetHeight = 2;

    public Task OnLoadAsync(CancellationToken cancellationToken)
    {
        var matchingMagazineCount = 0;
        var modifiedMagazineCount = 0;

        foreach (var item in templateTable.Items.Values)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var properties = item.Properties;
            if (properties is null)
            {
                continue;
            }

            var isFortyRoundMagazine =
                properties.ReloadMagType is not null
                && properties.Cartridges?.Any(slot => slot.MaxCount == TargetCapacity) == true;

            if (!isFortyRoundMagazine)
            {
                continue;
            }

            matchingMagazineCount++;

            if (properties.Height != OriginalHeight)
            {
                continue;
            }

            properties.Height = TargetHeight;
            modifiedMagazineCount++;
        }

        logger.Success(
            $"Two Slot Big Mags: {modifiedMagazineCount} chargeur(s) modifie(s) sur "
                + $"{matchingMagazineCount} chargeur(s) de {TargetCapacity} coups detecte(s)."
        );

        return Task.CompletedTask;
    }
}
