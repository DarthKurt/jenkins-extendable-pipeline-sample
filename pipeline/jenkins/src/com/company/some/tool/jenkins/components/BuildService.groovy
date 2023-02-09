package com.company.some.tool.jenkins.components

import com.company.some.tool.jenkins.BuildDotNetPipelineComponent
import com.company.some.tool.jenkins.CoreOperations
import org.jenkinsci.plugins.workflow.cps.CpsScript

final class BuildService extends BuildDotNetPipelineComponent {

    BuildService(final CoreOperations core, final CpsScript script) {
        super(core, script, 'src/Some.Company.Tool.EnvironmentsApi', 'environments-api')
    }
}
