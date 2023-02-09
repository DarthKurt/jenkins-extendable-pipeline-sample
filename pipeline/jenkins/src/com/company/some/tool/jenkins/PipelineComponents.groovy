package com.company.some.tool.jenkins

import com.company.some.tool.jenkins.components.BuildCLI
import com.company.some.tool.jenkins.components.BuildService

import groovy.transform.TypeChecked
import org.jenkinsci.plugins.workflow.cps.CpsScript
import org.jenkinsci.plugins.workflow.cps.EnvActionImpl

@TypeChecked
final class PipelineComponents {
    private final CpsScript script
    private final LinkedHashMap<String, BasePipelineComponent> pipelineComponents
    private final CoreOperations core

    PipelineComponents(final CpsScript script) {
        this.script = script
        this.core = new CoreOperations(script)
        this.pipelineComponents = [
                "1.0.0" : new BuildCLI(core, script),
                "2.0.0" : new BuildService(core, script)
        ]
    }

    final executePipelineComponent(final String componentName, final EnvActionImpl env) {
        if (this.pipelineComponents.containsKey(componentName)) {
            final IPipelineComponent component = this.pipelineComponents[componentName]
            component.run(env)
            return
        }

        this.script.error ("Component with name '${componentName}' was not defined")
    }
}
