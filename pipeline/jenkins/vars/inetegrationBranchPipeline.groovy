#!groovy
import com.company.some.tool.jenkins.PipelineComponents

def call(final PipelineComponents components, final String branchName) {
    pipeline {
        agent none
        environment {
            BRANCH_NAME = "${branchName}"
        }
        options {
            durabilityHint('PERFORMANCE_OPTIMIZED')
        }
        stages {
            // You can compose stages according branch or other conditions
            stage("[Integration] Build") {
                parallel {
                    stage("[Integration] Build dotnet CLI mock") {
                        agent any
                        steps {
                            script {
                                components.executePipelineComponent("1.0.0", env)
                            }
                        }
                    }
                    stage("[Integration] Build Environment API") {
                        agent any
                        steps {
                            script {
                                components.executePipelineComponent("2.0.0", env)
                            }
                        }
                    }
                }
            }
        }
    }
}
