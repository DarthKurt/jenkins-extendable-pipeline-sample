package com.company.some.tool.jenkins

import groovy.transform.CompileStatic
import org.jenkinsci.plugins.workflow.cps.EnvActionImpl

@CompileStatic
interface IPipelineComponent {
    void run(final EnvActionImpl env)
}

