#!groovy
import com.company.some.tool.jenkins.PipelineComponents
import groovy.transform.TypeChecked
import org.jenkinsci.plugins.workflow.cps.CpsScript
import com.company.some.tool.jenkins.constants.BranchNames
import hudson.plugins.git.GitSCM

@TypeChecked
static def call(final CpsScript cps) {
    final String branchName = resolveGitBranchName(cps)
    final PipelineComponents components = new PipelineComponents(cps)
    switch(branchName) {
        case BranchNames.MAIN_BRANCH_NAME:
            final def runner = new mainPipeline()
            runner(components, branchName)
            break
       case BranchNames.INT_BRANCH_NAME:
           final def runner = new inetegrationBranchPipeline()
//            runner(components, branchName)
           break
        default:
            final def runner = new topicPipeline()
            runner(components, branchName)
            break
    }
}


@TypeChecked
private static String resolveGitBranchName(final CpsScript cps) {
    def git = cps.scm as GitSCM

    if(git != null && git.branches.size() > 0) {
        return git.branches[0].name
    } else {
        cps.error(
            "This pipeline is designed to work with GitSCM. Looks like it was not used during checkout."
        )
    }
}
