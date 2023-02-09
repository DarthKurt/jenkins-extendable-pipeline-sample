#!groovy

package com.company.some.tool.jenkins.constants

import groovy.transform.CompileStatic

@CompileStatic
final class BranchNames {
    public static final String MAIN_BRANCH_NAME  = 'main'
    public static final String INT_BRANCH_NAME   = 'develop'
}

