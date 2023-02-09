package com.company.some.tool.jenkins

import groovy.transform.CompileStatic
import org.jenkinsci.plugins.workflow.cps.CpsScript

@CompileStatic
abstract class BasePipelineComponent implements IPipelineComponent {
    protected final CoreOperations core
    protected final CpsScript script

    BasePipelineComponent(final CoreOperations core, final CpsScript script) {
        this.script = script
        this.core = core
    }
}
