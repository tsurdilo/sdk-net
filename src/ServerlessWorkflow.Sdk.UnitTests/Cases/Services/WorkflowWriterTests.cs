﻿/*
 * Copyright 2021-Present The Serverless Workflow Specification Authors
 * <p>
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * <p>
 * http://www.apache.org/licenses/LICENSE-2.0
 * <p>
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */
using ServerlessWorkflow.Sdk.Models;
using ServerlessWorkflow.Sdk.Services.IO;
using System;
using System.IO;
using Xunit;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Services
{

    public class WorkflowWriterTests
    {

        protected IWorkflowWriter Writer { get; } = WorkflowWriter.Create();

        protected IWorkflowReader Reader { get; } = WorkflowReader.Create();

        protected WorkflowDefinition BuildWorkflow()
        {
            return WorkflowDefinition.Create("MyWorkflow", "MyWorkflow", "1.0")
                .WithExecutionTimeout(timeout =>
                    timeout.After(new TimeSpan(30, 2, 0, 0)))
                .StartsWith("inject", flow =>
                    flow.Inject(new { username = "test", password = "123456", scopes = new string[] { "api", "test" } }))
                .Then("operation", flow =>
                    flow.Execute("fakeApiFunctionCall", action =>
                    {
                        action.Invoke(function =>
                            function.WithName("fakeFunction")
                                .SetOperationUri(new Uri("https://fake.com/swagger.json#fake")))
                            .WithArgument("username", "${ .username }")
                            .WithArgument("password", "${ .password }");
                    })
                        .Execute("fakeEventTrigger", action =>
                        {
                            action.Consume(e =>
                                e.WithName("fakeEvent")
                                    .WithSource(new Uri("https://fakesource.com"))
                                    .WithType("fakeType"));
                        }))
                .End()
                .Build();
        }

        [Fact]
        public void WriteYaml()
        {
            var workflow = this.BuildWorkflow();
            var yaml = "";
            using(Stream stream = new MemoryStream())
            {
                this.Writer.Write(workflow, stream);
                stream.Flush();
                stream.Position = 0;
                using(StreamReader reader = new StreamReader(stream))
                {
                    yaml = reader.ReadToEnd();
                    stream.Position = 0;
                    workflow = this.Reader.Read(stream);
                    Assert.NotNull(workflow);
                }
            }
        }

        [Fact]
        public void WriteJson()
        {
            var workflow = this.BuildWorkflow();
            var json = "";
            using (Stream stream = new MemoryStream())
            {
                this.Writer.Write(workflow, stream, WorkflowDefinitionFormat.Json);
                stream.Flush();
                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream))
                {
                    json = reader.ReadToEnd();
                    stream.Position = 0;
                    workflow = this.Reader.Read(stream, WorkflowDefinitionFormat.Json);
                    Assert.NotNull(workflow);
                }
            }
        }

    }

}
