"use client";

import { useState } from "react";

import createCache from "@emotion/cache";
import { CacheProvider } from "@emotion/react";

import { useServerInsertedHTML } from "next/navigation";

import {
  CssBaseline,
  Experimental_CssVarsProvider as CssVarsProvider,
  getInitColorSchemeScript,
} from "@mui/material";

import { theme } from "./theme";

type Props = { children: React.ReactNode };

const ThemeRegistry = (props: Props) => {
  const [{ cache, flush }] = useState(() => {
    const cache = createCache({ key: "vibooks" });
    cache.compat = true;
    const prevInsert = cache.insert;
    let inserted: string[] = [];
    cache.insert = (...args) => {
      const serialized = args[1];
      if (cache.inserted[serialized.name] === undefined) {
        inserted.push(serialized.name);
      }
      return prevInsert(...args);
    };
    const flush = () => {
      const prevInserted = inserted;
      inserted = [];
      return prevInserted;
    };
    return { cache, flush };
  });

  useServerInsertedHTML(() => {
    const names = flush();
    if (names.length === 0) {
      return null;
    }
    let styles = "";
    for (const name of names) {
      styles += cache.inserted[name];
    }
    return (
      <style
        key={cache.key}
        data-emotion={`${cache.key} ${names.join(" ")}`}
        dangerouslySetInnerHTML={{
          __html: styles,
        }}
      />
    );
  });

  return (
    <>
      <CacheProvider value={cache}>
        {getInitColorSchemeScript({
          defaultMode: "system",
        })}
        <CssVarsProvider theme={theme} defaultMode="system">
          <CssBaseline />
          {props.children}
        </CssVarsProvider>
      </CacheProvider>
    </>
  );
};

export default ThemeRegistry;
