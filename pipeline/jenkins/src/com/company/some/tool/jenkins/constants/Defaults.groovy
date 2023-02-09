#!groovy

package com.company.some.tool.jenkins.constants

import groovy.transform.CompileStatic

@CompileStatic
final class Defaults {
    public static final String DEFAULT_NET_SDK_IMAGE    = 'mcr.microsoft.com/dotnet/sdk:7.0-alpine'
    public static final String DEFAULT_NODE_IMAGE       = 'node:18-alpine3.17'
}
