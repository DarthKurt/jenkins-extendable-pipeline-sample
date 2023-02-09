import { Command } from "commander";

export const test = new Command("test");

test
    .argument("<project>", "path to project")
    .description('Build a .NET project.')
    .option("--nologo", "Run test(s), without displaying Microsoft Testplatform banner")
    .option("--no-build", "Do not build the project before testing. Implies --no-restore.")
    .option("-l, --logger <LOGGER>", "The logger to use for test results."
        + "\n\tExamples:"
        + "\n\t\tLog in trx format using a unique file name: --logger trx"
        + "\n\t\t\tLog in trx format using the specified file name: --logger"
        + "\n\t\tSee https://aka.ms/vstest-report for more information on logger arguments.")
    .option("-r, --results-directory <RESULTS_DIR>", "The directory where the test results will be placed.")
    .option("--collect <DATA_COLLECTOR_NAME>", "The friendly name of the data collector to use for the test run.")
    .action((project: string, options: object) => {
        console.log('Run tests on project(s): %s with options:', project)
        console.table(options)
    });
