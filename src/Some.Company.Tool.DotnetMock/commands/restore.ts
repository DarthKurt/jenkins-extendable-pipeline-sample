import { Command } from "commander";
import * as fs from 'fs';
import path = require("path");

export const restore = new Command("restore");

restore
    .argument("<project>", "path to project")
    .description('Restore dependencies specified in a .NET project.')
    .option("-c, --configfile <path>", "Nuget.config file path")
    .action((project: string, options: object) => {
        console.log('Restoring project(s): %s with options:', project);
        console.table(options);

        const projectPath = path.resolve(project);
        const projectDir = path.dirname(projectPath);
        const assetsPath = path.resolve(projectDir, 'project.assets.json');

        // Simulate resoration for snyk
        if (fs.existsSync(projectDir)) {
            fs.writeFile(assetsPath, "{}", (err) => {
                if (err) {
                    throw err;
                }
                console.log(`Created: ${assetsPath}`);
            });
        }
    });
