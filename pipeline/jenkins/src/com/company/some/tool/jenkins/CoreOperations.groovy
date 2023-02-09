#!groovy

package com.company.some.tool.jenkins

import com.company.some.tool.jenkins.constants.Defaults
import groovy.transform.TypeChecked
import org.jenkinsci.plugins.workflow.cps.CpsScript

@TypeChecked
final class CoreOperations {

    private final CpsScript script

    /**
     * Create a new instance of the {@link CoreOperations}
     * @param script Object that represents Jenkins CPS script containing available commands.
     */
    CoreOperations(final CpsScript script) {
        this.script = script
    }

    /**
     * Build image with dotnet app.
     * @param workspacePath Path to the workspace root
     * @param branchName Name of the branch for which the image is building
     * @param artifactName Name of the image for the component
     * @param projectPath Name of the image for the component
     * @param tag Docker tag for the component indicating current version
     * @param SDK Image of the dotnet SDK used to build the component
     */
    void dotnetDocker(
            final String workspacePath,
            final String branchName,
            final String projectPath,
            final String artifactName,
            final String tag,
            final String SDK = Defaults.DEFAULT_NET_SDK_IMAGE
    ) {
        final String command = """${workspacePath}/pipeline/scripts/build-dotnet-docker.sh \\
                                    -b ${branchName} \\
                                    -i ${artifactName} \\
                                    -t ${tag} \\
                                    -s ${SDK} \\
                                    -p ${projectPath} \\
                                    -u https://github.com/DarthKurt/jenkins-extendable-pipeline-sample/${projectPath}
        """
        this.runCommand('Build .NET component\'s image', command)
    }

    /**
     * Build image with Node app.
     * @param workspacePath Path to the workspace root
     * @param branchName Name of the branch for which the image is building
     * @param artifactName Name of the image for the component
     * @param projectPath Name of the image for the component
     * @param tag Docker tag for the component indicating current version
     * @param SDK Image of the Node JS used to build the component
     */
    void nodeDocker(
            final String workspacePath,
            final String branchName,
            final String projectPath,
            final String artifactName,
            final String tag,
            final String SDK = Defaults.DEFAULT_NODE_IMAGE
    ) {
        final String command = """${workspacePath}/pipeline/scripts/build-dotnet-docker.sh \\
                                    -b ${branchName} \\
                                    -i ${artifactName} \\
                                    -t ${tag} \\
                                    -s ${SDK} \\
                                    -p ${projectPath} \\
                                    -u https://github.com/DarthKurt/jenkins-extendable-pipeline-sample/${projectPath}
        """
        this.runCommand('Build Node component\'s image', command)
    }

    /**
     * Just a small wrapper around script step to decrease verbosity
     */
    private runCommand(final String label, final String command) {
        this.script.sh([
                'label': label,
                'script': command
        ])
    }
}
