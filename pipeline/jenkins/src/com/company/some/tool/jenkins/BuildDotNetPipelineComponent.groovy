package com.company.some.tool.jenkins

import groovy.transform.TypeChecked
import org.jenkinsci.plugins.workflow.cps.CpsScript
import org.jenkinsci.plugins.workflow.cps.EnvActionImpl

@TypeChecked
abstract class BuildDotNetPipelineComponent extends BaseProjectPipelineComponent {

    BuildDotNetPipelineComponent(
            final CoreOperations core, final CpsScript script, String projectPath, String projectImageName) {
        super(core, script, projectPath, projectImageName)
    }

    @Override
    void run(EnvActionImpl env) {
        this.core.dotnetDocker(env.WORKSPACE, env.BRANCH_NAME, projectPath, projectImageName, env.TAG)
    }
}
