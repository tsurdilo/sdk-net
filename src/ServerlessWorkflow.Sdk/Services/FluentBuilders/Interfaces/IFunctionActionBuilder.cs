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
using System.Collections.Generic;

namespace ServerlessWorkflow.Sdk.Services.FluentBuilders
{
    /// <summary>
    /// Defines the service used to build <see cref="ActionDefinition"/>s of type <see cref="ActionType.FunctionCall"/>
    /// </summary>
    public interface IFunctionActionBuilder
        : IActionBuilder
    {

        /// <summary>
        /// Configures the <see cref="ActionDefinition"/> to use the specified argument when performing the function call
        /// </summary>
        /// <param name="name">The name of the argument to add</param>
        /// <param name="value">The value or workflow expression of the argument to add</param>
        /// <returns>The configured <see cref="IFunctionActionBuilder"/></returns>
        IFunctionActionBuilder WithArgument(string name, string value);

        /// <summary>
        /// Configures the <see cref="ActionDefinition"/> to use the specified argument when performing the function call
        /// </summary>
        /// <param name="args">An <see cref="IDictionary{TKey, TValue}"/> containing the name/value pairs of the arguments to use</param>
        /// <returns>The configured <see cref="IFunctionActionBuilder"/></returns>
        IFunctionActionBuilder WithArguments(IDictionary<string, string> args);

    }

}
