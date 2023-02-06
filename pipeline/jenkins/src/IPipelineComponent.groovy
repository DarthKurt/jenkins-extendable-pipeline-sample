package com.company.some.tool.jenkins

import org.jenkinsci.plugins.workflow.cps.EnvActionImpl

interface IPipelineComponent {
    void run(final EnvActionImpl env)
}

