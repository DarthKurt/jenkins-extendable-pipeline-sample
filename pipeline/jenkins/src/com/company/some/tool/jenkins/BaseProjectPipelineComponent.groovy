package com.company.some.tool.jenkins

import groovy.transform.TypeChecked
import org.jenkinsci.plugins.workflow.cps.CpsScript

@TypeChecked
abstract class BaseProjectPipelineComponent extends BasePipelineComponent {

    protected final String projectPath
    protected final String projectImageName

    BaseProjectPipelineComponent(
            final CoreOperations core, final CpsScript script, String projectPath, String projectImageName) {
        super(core, script)
        this.projectPath = projectPath
        this.projectImageName = projectImageName
    }
}
