import { Command } from "commander";

export const build = new Command("build");

build
    .argument("<project>", "path to project")
    .description('Build a .NET project.')
    .option("--nologo", "Do not display the startup banner or the copyright message.")
    .option("--no-restore", "Do not restore the project before building.")
    .option("-v, --verbosity <LEVEL> ", "Set the MSBuild verbosity level. Allowed values are q[uiet], m[inimal], n[ormal], d[etailed], and diag[nostic].")
    .action((project: string, options: object) => {
        console.log('Build project(s): %s with options:', project)
        console.table(options)
     });
