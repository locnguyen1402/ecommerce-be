"use client";

import { useState } from "react";

import { useRouter } from "next/navigation";

import { Button, InputAdornment, Stack, TextField } from "@mui/material";
import { SearchOutlined as SearchIcon } from "@mui/icons-material";

const FilterSection = () => {
  const [searchText, setSearchText] = useState("");
  const router = useRouter();

  const onApply = () => {
    router.push(`/search?keyword=${searchText}`);
  };

  return (
    <Stack width="var(--filter-section-width)" spacing={2}>
      <TextField
        value={searchText}
        onChange={(evt) => setSearchText(evt.target.value)}
        size="small"
        variant="outlined"
        InputProps={{
          startAdornment: (
            <InputAdornment position="start">
              <SearchIcon />
            </InputAdornment>
          ),
        }}
      />
      <Button onClick={onApply}>Apply</Button>
    </Stack>
  );
};

export default FilterSection;
