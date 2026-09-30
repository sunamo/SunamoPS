---
schema_version: 2
type: library
file_count: 56
delete_recommendation_percent: 10
generated_date: 2026-09-30
generated_time: 15:11:44
---

## Description

Knihovna pro práci s PowerShell 7: spouštění příkazů a skriptů (`PowershellRunner`, `PowershellBuilder`), parsování a zpracování výstupů (`PowershellParser`, `PsOutput`, `ErrorRecordHelper`). Staví na `Microsoft.PowerShell.SDK` a `System.Management.Automation`.
Balíček je self-contained: kód dříve referencovaných balíčků (SunamoExceptions, SunamoStringGetLines, SunamoStringSplit, SunamoDictionary, SunamoInterfaces aj.) je zkopírován do `_sunamo\` jako internal; jiné Sunamo balíčky z projektu SunamoPS nereferencuje (pomocný projekt RunnerPS má ještě project reference).
