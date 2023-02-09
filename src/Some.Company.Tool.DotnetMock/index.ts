import { Command } from 'commander'
import path = require('path');
import { build } from './commands/build';
import { restore } from './commands/restore';
import { test } from './commands/test';

const logCommand = (command: string) => {
    console.log('\033[35mCommand called: %s\033[0m\n', command)
}

async function main(entrypoint: Command) {
  await entrypoint.parseAsync();
}

const program = new Command();
program
  .description('Mock of the dotnet CLI')
  .version('1.0.0')
  .addCommand(build)
  .addCommand(restore)
  .addCommand(test)
  .configureHelp({
    sortSubcommands: true,
  })
  .addHelpCommand(false)
  .showHelpAfterError(true);

const commandName = path.basename(__filename);
const args = process.argv.slice(2).join(' ');

logCommand(`${commandName} ${args}`)

main(program);
