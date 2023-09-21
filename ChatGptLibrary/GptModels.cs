using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ChatGptLibrary
{
    

    public enum GPTModels
    {
        [Description("gpt-3.5-turbo")]
        GPT3_5Turbo,

        [Description("gpt-3.5-turbo-16k")]
        GPT3_5Turbo16K,

        [Description("gpt-3.5-turbo-instruct")]
        GPT3_5TurboInstruct,

        [Description("gpt-3.5-turbo-0613")]
        GPT3_5Turbo0613,

        [Description("gpt-3.5-turbo-16k-0613")]
        GPT3_5Turbo16K0613,

        [Description("gpt-3.5-turbo-0301 (Legacy)")]
        GPT3_5Turbo0301Legacy,

        [Description("text-davinci-003 (Legacy)")]
        TextDavinci003Legacy,

        [Description("text-davinci-002 (Legacy)")]
        TextDavinci002Legacy,

        [Description("code-davinci-002 (Legacy)")]
        CodeDavinci002Legacy,

        [Description("gpt-4")]
        GPT4,

        [Description("gpt-4-0613")]
        GPT40613,

        [Description("gpt-4-32k")]
        GPT432K,

        [Description("gpt-4-32k-0613")]
        GPT432K0613,

        [Description("gpt-4-0314 (Legacy)")]
        GPT40314Legacy,

        [Description("gpt-4-32k-0314 (Legacy)")]
        GPT432K0314Legacy
    }

}
