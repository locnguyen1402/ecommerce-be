"use client";

import { useState, ReactNode } from "react";
import { useRecoilValue } from "recoil";

import createCache from "@emotion/cache";
import { CacheProvider } from "@emotion/react";

import { useServerInsertedHTML } from "next/navigation";

import {
  CssBaseline,
  Experimental_CssVarsProvider as CssVarsProvider,
  getInitColorSchemeScript,
} from "@mui/material";

import { layoutStateSelector } from "@/stores/selectors";

import { buildTheme } from "./theme";

type Props = { children: ReactNode };

const ThemeRegistry = (props: Props) => {
  const layoutState = useRecoilValue(layoutStateSelector);

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
        <CssVarsProvider
          theme={buildTheme({ primary: layoutState.palette.primary })}
          defaultMode="system"
        >
          <CssBaseline />
          {props.children}
        </CssVarsProvider>
      </CacheProvider>
    </>
  );
};

export default ThemeRegistry;
