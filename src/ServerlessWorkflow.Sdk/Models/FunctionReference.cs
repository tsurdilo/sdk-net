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
using Newtonsoft.Json.Linq;
using YamlDotNet.Serialization;
using System.ComponentModel.DataAnnotations;

namespace ServerlessWorkflow.Sdk.Models
{

    /// <summary>
    /// Represents a reference to a <see cref="FunctionDefinition"/>
    /// </summary>
    public class FunctionReference
    {

        /// <summary>
        /// Gets/sets the referenced function's name
        /// </summary>
        [Required]
        [Newtonsoft.Json.JsonProperty(PropertyName = "refName"), Newtonsoft.Json.JsonRequired]
        [System.Text.Json.Serialization.JsonPropertyName("refName")]
        [YamlMember(Alias = "refName")]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets/sets a <see cref="JObject"/> that contains the parameters of the function to invoke
        /// </summary>
        public virtual JObject Arguments { get; set; } = new JObject();

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.Name;
        }

    }

}