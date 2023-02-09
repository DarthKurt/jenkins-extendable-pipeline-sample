package com.company.some.tool.jenkins.components

import com.company.some.tool.jenkins.BuildNodePipelineComponent
import com.company.some.tool.jenkins.CoreOperations
import org.jenkinsci.plugins.workflow.cps.CpsScript

final class BuildCLI extends BuildNodePipelineComponent {

    BuildCLI(final CoreOperations core, final CpsScript script) {
        super(core, script, 'src/Some.Company.Tool.DotnetMock', 'dotnet-mock')
    }
}
